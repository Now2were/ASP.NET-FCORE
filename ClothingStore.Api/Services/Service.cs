using AutoMapper;
using ClothingStore.Api.Common;
using ClothingStore.Api.DTO;
using ClothingStore.Api.Repositories;
using System.ComponentModel.DataAnnotations;
using AppException = ClothingStore.Api.Common.ApplicationException;

namespace ClothingStore.Api.Services;

public class Service<TEntity, TRead, TCreate, TUpdate, TPatch>(IRepository<TEntity> repository, IMapper mapper)
    : IService<TRead, TCreate, TUpdate, TPatch>
    where TEntity : class where TPatch : PatchDto
{
    protected readonly IRepository<TEntity> Repository = repository;
    protected readonly IMapper Mapper = mapper;

    public async Task<PagedResult<TRead>> GetPageAsync(int page, int pageSize, CancellationToken ct)
    {
        if (page < 1 || pageSize < 1 || pageSize > 100 || (long)(page - 1) * pageSize > int.MaxValue)
            throw new AppException(ErrorKind.Validation, "page должен быть >= 1, pageSize от 1 до 100; смещение не должно превышать Int32.");
        var result = await Repository.GetPageAsync(page, pageSize, ct);
        return new(Mapper.Map<List<TRead>>(result.Items), result.TotalCount);
    }

    protected async Task<TEntity> FindAsync(object[] keys, CancellationToken ct) =>
        await Repository.GetAsync(keys, ct) ?? throw new AppException(ErrorKind.NotFound, "Запись не найдена.");

    public async Task<TRead> GetAsync(object[] keys, CancellationToken ct) => Mapper.Map<TRead>(await FindAsync(keys, ct));

    public virtual async Task<TRead> CreateAsync(TCreate dto, CancellationToken ct)
    {
        ValidateDto(dto!);
        var entity = Mapper.Map<TEntity>(dto);
        SetTimestamp(entity, "CreatedAt", DateTime.UtcNow);
        await ValidateEntityAsync(entity, ct);
        Repository.Add(entity);
        await Repository.SaveChangesAsync(ct);
        return Mapper.Map<TRead>(await ReloadAsync(entity, ct));
    }

    public virtual async Task UpdateAsync(object[] keys, TUpdate dto, CancellationToken ct)
    {
        var entity = await FindAsync(keys, ct);
        ValidateDto(dto!);
        Mapper.Map(dto, entity);
        SetTimestamp(entity, "UpdatedAt", DateTime.UtcNow);
        await ValidateEntityAsync(entity, ct);
        await Repository.SaveChangesAsync(ct);
    }

    public virtual async Task PatchAsync(object[] keys, TPatch dto, CancellationToken ct)
    {
        var entity = await FindAsync(keys, ct);
        ValidatePatch(dto);
        Mapper.Map(dto, entity);
        ValidateDto(Mapper.Map<TUpdate>(entity)!);
        SetTimestamp(entity, "UpdatedAt", DateTime.UtcNow);
        await ValidateEntityAsync(entity, ct);
        await Repository.SaveChangesAsync(ct);
    }

    public virtual async Task DeleteAsync(object[] keys, CancellationToken ct)
    {
        var entity = await FindAsync(keys, ct);
        Repository.Remove(entity);
        await Repository.SaveChangesAsync(ct);
    }

    private async Task<TEntity> ReloadAsync(TEntity entity, CancellationToken ct)
    {
        var id = typeof(TEntity).GetProperty("Id");
        var keys = id is not null ? new[] { id.GetValue(entity)! } : new[] {
            typeof(TEntity).GetProperty("UserId")!.GetValue(entity)!, typeof(TEntity).GetProperty("RoleId")!.GetValue(entity)! };
        return await FindAsync(keys, ct);
    }

    protected async Task ValidateEntityAsync(TEntity entity, CancellationToken ct)
    {
        EntityRules.Validate(entity);
        await Repository.ValidateAsync(entity, ct);
    }

    protected static void ValidatePatch(TPatch dto)
    {
        ValidateDto(dto);
        foreach (var name in dto.ProvidedProperties)
        {
            var property = typeof(TPatch).GetProperty(name)!;
            var target = typeof(TEntity).GetProperty(name)!;
            if (property.GetValue(dto) is null && target.PropertyType.IsValueType && Nullable.GetUnderlyingType(target.PropertyType) is null)
                throw new AppException(ErrorKind.Validation, $"Поле {name} не может быть null.");
        }
    }

    public static void ValidateDto(object dto)
    {
        var errors = new List<ValidationResult>();
        if (!Validator.TryValidateObject(dto, new ValidationContext(dto), errors, true))
            throw new AppException(ErrorKind.Validation, string.Join(" ", errors.Select(x => x.ErrorMessage)));
    }

    private static void SetTimestamp(TEntity entity, string name, DateTime value) =>
        typeof(TEntity).GetProperty(name)?.SetValue(entity, value);
}
