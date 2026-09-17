using ClothingStore.Api.Data;
using ClothingStore.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly EntityApiContext _context;

    public CategoriesController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/categories?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var categorys = await _context.Categories
            .OrderBy(category => category.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return categorys.Select(CategoryMapper.ToDto).ToList();
    }

    // GET: api/categories/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> GetById(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(category => category.Id == id);

        if (category is null)
        {
            return NotFound();
        }

        return CategoryMapper.ToDto(category);
    }

    // POST: api/categories
    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoryDto>> Create(CreateCategoryDto dto)
    {
        var category = CategoryMapper.ToEntity(dto);

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = category.Id }, CategoryMapper.ToDto(category));
    }

    // PUT: api/categories/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(category => category.Id == id);

        if (category is null)
        {
            return NotFound();
        }

        category.ParentCategoryId = dto.ParentCategoryId;
        category.Name = dto.Name;
        category.Slug = dto.Slug;
        category.SortOrder = dto.SortOrder;
        category.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/categories/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchCategoryDto dto)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(category => category.Id == id);

        if (category is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.ParentCategoryId is not null)
        {
            category.ParentCategoryId = dto.ParentCategoryId.Value;
            hasChanges = true;
        }

        if (dto.Name is not null)
        {
            category.Name = dto.Name;
            hasChanges = true;
        }

        if (dto.Slug is not null)
        {
            category.Slug = dto.Slug;
            hasChanges = true;
        }

        if (dto.SortOrder is not null)
        {
            category.SortOrder = dto.SortOrder.Value;
            hasChanges = true;
        }

        if (dto.IsActive is not null)
        {
            category.IsActive = dto.IsActive.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/categories/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(category => category.Id == id);

        if (category is null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}