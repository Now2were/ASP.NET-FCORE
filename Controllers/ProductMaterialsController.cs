using ClothingStore.Api.Data;
using ClothingStore.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/product-materials")]
public class ProductMaterialsController : ControllerBase
{
    private readonly EntityApiContext _context;

    public ProductMaterialsController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/product-materials?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductMaterialDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductMaterialDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var materials = await _context.ProductMaterials
            .OrderBy(material => material.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return materials.Select(ProductMaterialMapper.ToDto).ToList();
    }

    // GET: api/product-materials/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductMaterialDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductMaterialDto>> GetById(int id)
    {
        var material = await _context.ProductMaterials
            .FirstOrDefaultAsync(material => material.Id == id);

        if (material is null)
        {
            return NotFound();
        }

        return ProductMaterialMapper.ToDto(material);
    }

    // POST: api/product-materials
    [HttpPost]
    [ProducesResponseType(typeof(ProductMaterialDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductMaterialDto>> Create(CreateProductMaterialDto dto)
    {
        var material = ProductMaterialMapper.ToEntity(dto);

        _context.ProductMaterials.Add(material);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = material.Id }, ProductMaterialMapper.ToDto(material));
    }

    // PUT: api/product-materials/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateProductMaterialDto dto)
    {
        var material = await _context.ProductMaterials
            .FirstOrDefaultAsync(material => material.Id == id);

        if (material is null)
        {
            return NotFound();
        }

        material.ProductId = dto.ProductId;
        material.MaterialName = dto.MaterialName;
        material.Percent = dto.Percent;
        material.SortOrder = dto.SortOrder;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/product-materials/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchProductMaterialDto dto)
    {
        var material = await _context.ProductMaterials
            .FirstOrDefaultAsync(material => material.Id == id);

        if (material is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.ProductId is not null)
        {
            material.ProductId = dto.ProductId.Value;
            hasChanges = true;
        }

        if (dto.MaterialName is not null)
        {
            material.MaterialName = dto.MaterialName;
            hasChanges = true;
        }

        if (dto.Percent is not null)
        {
            material.Percent = dto.Percent.Value;
            hasChanges = true;
        }

        if (dto.SortOrder is not null)
        {
            material.SortOrder = dto.SortOrder.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/product-materials/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var material = await _context.ProductMaterials
            .FirstOrDefaultAsync(material => material.Id == id);

        if (material is null)
        {
            return NotFound();
        }

        _context.ProductMaterials.Remove(material);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}