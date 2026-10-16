using ClothingStore.Api.Common;
using ClothingStore.Api.DTO;
namespace ClothingStore.Api.Services;
public interface IStockReservationService
{
    Task<PagedResult<StockReservationDto>> GetPageAsync(int page, int pageSize, CancellationToken ct);
    Task<StockReservationDto> GetAsync(int id, CancellationToken ct);
    Task<StockReservationDto> ReserveAsync(CreateStockReservationDto dto, CancellationToken ct);
    Task<StockReservationDto> CancelAsync(int id, CancellationToken ct);
    Task UpdateAsync(int id, UpdateStockReservationDto dto, CancellationToken ct);
    Task PatchAsync(int id, PatchStockReservationDto dto, CancellationToken ct);
}
