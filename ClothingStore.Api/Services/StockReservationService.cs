using AutoMapper;
using ClothingStore.Api.Common;
using ClothingStore.Api.DTO;
using ClothingStore.Api.Models;
using ClothingStore.Api.Repositories;
using System.ComponentModel.DataAnnotations;
using AppException = ClothingStore.Api.Common.ApplicationException;
namespace ClothingStore.Api.Services;

public class StockReservationService(IRepository<StockReservation> reservations,
    IRepository<ProductVariant> variants, IRepository<User> users, IMapper mapper) : IStockReservationService
{
    public async Task<PagedResult<StockReservationDto>> GetPageAsync(int page, int pageSize, CancellationToken ct)
    {
        if (page < 1 || pageSize < 1 || pageSize > 100 || (long)(page - 1) * pageSize > int.MaxValue)
            throw new AppException(ErrorKind.Validation, "page >= 1, pageSize от 1 до 100; смещение не должно превышать Int32.");
        var result = await reservations.GetPageAsync(page, pageSize, ct);
        return new(mapper.Map<List<StockReservationDto>>(result.Items), result.TotalCount);
    }
    public async Task<StockReservationDto> GetAsync(int id, CancellationToken ct) => mapper.Map<StockReservationDto>(
        await reservations.GetAsync([id], ct) ?? throw new AppException(ErrorKind.NotFound, "Резерв не найден."));

    public async Task<StockReservationDto> ReserveAsync(CreateStockReservationDto dto, CancellationToken ct)
    {
        Validate(dto);
        var user = await GetActiveUserAsync(dto.UserId, ct);
        var variant = await GetVariantAsync(dto.VariantId, ct);
        CheckAvailability(variant, dto.Quantity);
        var reservation = mapper.Map<StockReservation>(dto);
        reservation.CreatedAt = DateTime.UtcNow;
        reservation.User = user;
        reservation.Variant = variant;
        variant.ReservedQuantity += dto.Quantity;
        reservations.Add(reservation);
        // Один SaveChangesAsync сохраняет обе сущности атомарно в общей scoped-сессии EF.
        await reservations.SaveChangesAsync(ct);
        return mapper.Map<StockReservationDto>(reservation);
    }
    public async Task<StockReservationDto> CancelAsync(int id, CancellationToken ct)
    {
        var reservation = await GetActiveAsync(id, ct);
        var variant = reservation.Variant;
        if (variant.ReservedQuantity < reservation.Quantity)
            throw new AppException(ErrorKind.Conflict, "Зарезервированное количество меньше размера резерва. Проверьте остатки.");
        variant.ReservedQuantity -= reservation.Quantity;
        reservation.CancelledAt = DateTime.UtcNow;
        // Никакого отдельного UnitOfWork или явной транзакции.
        await reservations.SaveChangesAsync(ct);
        return mapper.Map<StockReservationDto>(reservation);
    }
    public async Task UpdateAsync(int id, UpdateStockReservationDto dto, CancellationToken ct)
    {
        var reservation = await GetActiveAsync(id, ct);
        Validate(dto);
        var user = await GetActiveUserAsync(dto.UserId, ct);
        var oldVariant = reservation.Variant;
        var newVariant = await GetVariantAsync(dto.VariantId, ct);
        if (oldVariant.ReservedQuantity < reservation.Quantity)
            throw new AppException(ErrorKind.Conflict, "Некорректный остаток резерва.");
        var available = newVariant.StockQuantity - newVariant.ReservedQuantity
            + (oldVariant.Id == newVariant.Id ? reservation.Quantity : 0);
        if (!newVariant.IsActive || !newVariant.Product.IsActive || available < dto.Quantity)
            throw new AppException(ErrorKind.Conflict, $"Вариант недоступен либо недостаточно товара: доступно {available}, требуется {dto.Quantity}.");
        oldVariant.ReservedQuantity -= reservation.Quantity;
        newVariant.ReservedQuantity += dto.Quantity;
        mapper.Map(dto, reservation);
        reservation.User = user;
        reservation.Variant = newVariant;
        await reservations.SaveChangesAsync(ct);
    }
    public async Task PatchAsync(int id, PatchStockReservationDto dto, CancellationToken ct)
    {
        var reservation = await GetActiveAsync(id, ct);
        foreach (var name in dto.ProvidedProperties)
            if (typeof(PatchStockReservationDto).GetProperty(name)!.GetValue(dto) is null)
                throw new AppException(ErrorKind.Validation, $"Поле {name} не может быть null.");
        var update = mapper.Map<UpdateStockReservationDto>(reservation);
        mapper.Map(dto, update);
        await UpdateAsync(id, update, ct);
    }
    private async Task<StockReservation> GetActiveAsync(int id, CancellationToken ct)
    {
        var reservation = await reservations.GetAsync([id], ct);
        if (reservation is null || reservation.CancelledAt is not null)
            throw new AppException(ErrorKind.NotFound, "Активный резерв не найден.");
        return reservation;
    }
    private async Task<User> GetActiveUserAsync(int id, CancellationToken ct)
    {
        var user = await users.GetAsync([id], ct) ?? throw new AppException(ErrorKind.NotFound, "Пользователь не найден.");
        if (!user.IsActive) throw new AppException(ErrorKind.Validation, "Пользователь неактивен.");
        return user;
    }
    private async Task<ProductVariant> GetVariantAsync(int id, CancellationToken ct) =>
        await variants.GetAsync([id], ct) ?? throw new AppException(ErrorKind.NotFound, "Вариант товара не найден.");
    private static void CheckAvailability(ProductVariant variant, int quantity)
    {
        if (!variant.IsActive || !variant.Product.IsActive)
            throw new AppException(ErrorKind.Conflict, "Товар или вариант неактивен.");
        var available = variant.StockQuantity - variant.ReservedQuantity;
        if (available < quantity)
            throw new AppException(ErrorKind.Conflict, $"Недостаточно товара: доступно {available}, запрошено {quantity}.");
    }
    private static void Validate(object dto)
    {
        var errors = new List<ValidationResult>();
        if (!Validator.TryValidateObject(dto, new ValidationContext(dto), errors, true))
            throw new AppException(ErrorKind.Validation, string.Join(" ", errors.Select(e => e.ErrorMessage)));
    }
}
