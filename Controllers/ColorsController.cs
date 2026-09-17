using ClothingStore.Api.Data;
using ClothingStore.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/colors")]
public class ColorsController : ControllerBase
{
    private readonly EntityApiContext _context;

    public ColorsController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/colors?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ColorDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ColorDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var colors = await _context.Colors
            .OrderBy(color => color.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return colors.Select(ColorMapper.ToDto).ToList();
    }

    // GET: api/colors/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ColorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ColorDto>> GetById(int id)
    {
        var color = await _context.Colors
            .FirstOrDefaultAsync(color => color.Id == id);

        if (color is null)
        {
            return NotFound();
        }

        return ColorMapper.ToDto(color);
    }

    // POST: api/colors
    [HttpPost]
    [ProducesResponseType(typeof(ColorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ColorDto>> Create(CreateColorDto dto)
    {
        var color = ColorMapper.ToEntity(dto);

        _context.Colors.Add(color);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = color.Id }, ColorMapper.ToDto(color));
    }

    // PUT: api/colors/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateColorDto dto)
    {
        var color = await _context.Colors
            .FirstOrDefaultAsync(color => color.Id == id);

        if (color is null)
        {
            return NotFound();
        }

        color.Name = dto.Name;
        color.Code = dto.Code;
        color.Hex = dto.Hex;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/colors/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchColorDto dto)
    {
        var color = await _context.Colors
            .FirstOrDefaultAsync(color => color.Id == id);

        if (color is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.Name is not null)
        {
            color.Name = dto.Name;
            hasChanges = true;
        }

        if (dto.Code is not null)
        {
            color.Code = dto.Code;
            hasChanges = true;
        }

        if (dto.Hex is not null)
        {
            color.Hex = dto.Hex;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/colors/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var color = await _context.Colors
            .FirstOrDefaultAsync(color => color.Id == id);

        if (color is null)
        {
            return NotFound();
        }

        _context.Colors.Remove(color);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}