using System.Globalization;
using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/stock-reservations")]
public class StockReservationsController(IStockReservationService service) : ApiControllerBase
{
    [HttpGet]
    public Task<IActionResult> GetAll(int page = 1, int pageSize = 10, CancellationToken ct = default) => ExecuteAsync(async () =>
    {
        var result = await service.GetPageAsync(page, pageSize, ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString(CultureInfo.InvariantCulture);
        return Ok(result.Items);
    });
    [HttpGet("{id:int}")]
    public Task<IActionResult> GetById(int id, CancellationToken ct) => ExecuteAsync(async () => Ok(await service.GetAsync(id, ct)));
    [HttpPost]
    public Task<IActionResult> Reserve(CreateStockReservationDto dto, CancellationToken ct) => ExecuteAsync(async () =>
    {
        var result = await service.ReserveAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { result.Id }, result);
    });
    [HttpPost("{id:int}/cancel")]
    public Task<IActionResult> Cancel(int id, CancellationToken ct) => ExecuteAsync(async () => Ok(await service.CancelAsync(id, ct)));
    [HttpPut("{id:int}")]
    public Task<IActionResult> Update(int id, UpdateStockReservationDto dto, CancellationToken ct) => ExecuteAsync(async () =>
    { await service.UpdateAsync(id, dto, ct); return NoContent(); });
    [HttpPatch("{id:int}")]
    public Task<IActionResult> Patch(int id, PatchStockReservationDto dto, CancellationToken ct) => ExecuteAsync(async () =>
    { await service.PatchAsync(id, dto, ct); return NoContent(); });
    // Удаление резерва означает отмену: факт операции остаётся в истории.
    [HttpDelete("{id:int}")]
    public Task<IActionResult> Delete(int id, CancellationToken ct) => ExecuteAsync(async () =>
    { await service.CancelAsync(id, ct); return NoContent(); });
}
