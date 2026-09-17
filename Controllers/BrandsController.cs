using ClothingStore.Api.Data;
using ClothingStore.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/brands")]
public class BrandsController : ControllerBase
{
    private readonly EntityApiContext _context;

    public BrandsController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/brands?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BrandDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var brands = await _context.Brands
            .OrderBy(brand => brand.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return brands.Select(BrandMapper.ToDto).ToList();
    }

    // GET: api/brands/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BrandDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BrandDto>> GetById(int id)
    {
        var brand = await _context.Brands
            .FirstOrDefaultAsync(brand => brand.Id == id);

        if (brand is null)
        {
            return NotFound();
        }

        return BrandMapper.ToDto(brand);
    }

    // POST: api/brands
    [HttpPost]
    [ProducesResponseType(typeof(BrandDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BrandDto>> Create(CreateBrandDto dto)
    {
        var brand = BrandMapper.ToEntity(dto);

        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = brand.Id }, BrandMapper.ToDto(brand));
    }

    // PUT: api/brands/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateBrandDto dto)
    {
        var brand = await _context.Brands
            .FirstOrDefaultAsync(brand => brand.Id == id);

        if (brand is null)
        {
            return NotFound();
        }

        brand.Name = dto.Name;
        brand.Slug = dto.Slug;
        brand.Country = dto.Country;
        brand.Description = dto.Description;
        brand.LogoUrl = dto.LogoUrl;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/brands/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchBrandDto dto)
    {
        var brand = await _context.Brands
            .FirstOrDefaultAsync(brand => brand.Id == id);

        if (brand is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.Name is not null)
        {
            brand.Name = dto.Name;
            hasChanges = true;
        }

        if (dto.Slug is not null)
        {
            brand.Slug = dto.Slug;
            hasChanges = true;
        }

        if (dto.Country is not null)
        {
            brand.Country = dto.Country;
            hasChanges = true;
        }

        if (dto.Description is not null)
        {
            brand.Description = dto.Description;
            hasChanges = true;
        }

        if (dto.LogoUrl is not null)
        {
            brand.LogoUrl = dto.LogoUrl;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/brands/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var brand = await _context.Brands
            .FirstOrDefaultAsync(brand => brand.Id == id);

        if (brand is null)
        {
            return NotFound();
        }

        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}