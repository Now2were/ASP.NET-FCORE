using ClothingStore.Api.Data;
using ClothingStore.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/sizes")]
public class SizesController : ControllerBase
{
    private readonly EntityApiContext _context;

    public SizesController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/sizes?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<SizeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SizeDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var sizes = await _context.Sizes
            .OrderBy(size => size.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return sizes.Select(SizeMapper.ToDto).ToList();
    }

    // GET: api/sizes/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SizeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SizeDto>> GetById(int id)
    {
        var size = await _context.Sizes
            .FirstOrDefaultAsync(size => size.Id == id);

        if (size is null)
        {
            return NotFound();
        }

        return SizeMapper.ToDto(size);
    }

    // POST: api/sizes
    [HttpPost]
    [ProducesResponseType(typeof(SizeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SizeDto>> Create(CreateSizeDto dto)
    {
        var size = SizeMapper.ToEntity(dto);

        _context.Sizes.Add(size);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = size.Id }, SizeMapper.ToDto(size));
    }

    // PUT: api/sizes/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateSizeDto dto)
    {
        var size = await _context.Sizes
            .FirstOrDefaultAsync(size => size.Id == id);

        if (size is null)
        {
            return NotFound();
        }

        size.Name = dto.Name;
        size.SortOrder = dto.SortOrder;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/sizes/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchSizeDto dto)
    {
        var size = await _context.Sizes
            .FirstOrDefaultAsync(size => size.Id == id);

        if (size is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.Name is not null)
        {
            size.Name = dto.Name;
            hasChanges = true;
        }

        if (dto.SortOrder is not null)
        {
            size.SortOrder = dto.SortOrder.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/sizes/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var size = await _context.Sizes
            .FirstOrDefaultAsync(size => size.Id == id);

        if (size is null)
        {
            return NotFound();
        }

        _context.Sizes.Remove(size);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}