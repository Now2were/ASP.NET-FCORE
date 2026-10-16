using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;

public abstract class CrudController<TRead, TCreate, TUpdate, TPatch>(IService<TRead, TCreate, TUpdate, TPatch> service)
    : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default) => ExecuteAsync(async () =>
    {
        var result = await service.GetPageAsync(page, pageSize, ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString(CultureInfo.InvariantCulture);
        return Ok(result.Items);
    });

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> GetById(int id, CancellationToken ct) => ExecuteAsync(async () => Ok(await service.GetAsync([id], ct)));

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public Task<IActionResult> Create([FromBody] TCreate dto, CancellationToken ct) => ExecuteAsync(async () =>
    {
        var result = await service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = typeof(TRead).GetProperty("Id")!.GetValue(result) }, result);
    });

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> Update(int id, [FromBody] TUpdate dto, CancellationToken ct) => ExecuteAsync(async () =>
    { await service.UpdateAsync([id], dto, ct); return NoContent(); });

    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> Patch(int id, [FromBody] TPatch dto, CancellationToken ct) => ExecuteAsync(async () =>
    { await service.PatchAsync([id], dto, ct); return NoContent(); });

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task<IActionResult> Delete(int id, CancellationToken ct) => ExecuteAsync(async () =>
    { await service.DeleteAsync([id], ct); return NoContent(); });
}
