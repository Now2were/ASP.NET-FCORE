using ClothingStore.Api.Data;
using ClothingStore.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/product-variants")]
public class ProductVariantsController : ControllerBase
{
    private readonly EntityApiContext _context;

    public ProductVariantsController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/product-variants?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductVariantDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductVariantDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var variants = await _context.ProductVariants
            .OrderBy(variant => variant.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return variants.Select(ProductVariantMapper.ToDto).ToList();
    }

    // GET: api/product-variants/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductVariantDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductVariantDto>> GetById(int id)
    {
        var variant = await _context.ProductVariants
            .FirstOrDefaultAsync(variant => variant.Id == id);

        if (variant is null)
        {
            return NotFound();
        }

        return ProductVariantMapper.ToDto(variant);
    }

    // POST: api/product-variants
    [HttpPost]
    [ProducesResponseType(typeof(ProductVariantDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductVariantDto>> Create(CreateProductVariantDto dto)
    {
        var variant = ProductVariantMapper.ToEntity(dto);
        variant.CreatedAt = DateTime.UtcNow;

        _context.ProductVariants.Add(variant);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = variant.Id }, ProductVariantMapper.ToDto(variant));
    }

    // PUT: api/product-variants/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateProductVariantDto dto)
    {
        var variant = await _context.ProductVariants
            .FirstOrDefaultAsync(variant => variant.Id == id);

        if (variant is null)
        {
            return NotFound();
        }

        variant.ProductId = dto.ProductId;
        variant.SizeId = dto.SizeId;
        variant.ColorId = dto.ColorId;
        variant.Sku = dto.Sku;
        variant.StockQuantity = dto.StockQuantity;
        variant.ReservedQuantity = dto.ReservedQuantity;
        variant.PriceOverride = dto.PriceOverride;
        variant.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/product-variants/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchProductVariantDto dto)
    {
        var variant = await _context.ProductVariants
            .FirstOrDefaultAsync(variant => variant.Id == id);

        if (variant is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.ProductId is not null)
        {
            variant.ProductId = dto.ProductId.Value;
            hasChanges = true;
        }

        if (dto.SizeId is not null)
        {
            variant.SizeId = dto.SizeId.Value;
            hasChanges = true;
        }

        if (dto.ColorId is not null)
        {
            variant.ColorId = dto.ColorId.Value;
            hasChanges = true;
        }

        if (dto.Sku is not null)
        {
            variant.Sku = dto.Sku;
            hasChanges = true;
        }

        if (dto.StockQuantity is not null)
        {
            variant.StockQuantity = dto.StockQuantity.Value;
            hasChanges = true;
        }

        if (dto.ReservedQuantity is not null)
        {
            variant.ReservedQuantity = dto.ReservedQuantity.Value;
            hasChanges = true;
        }

        if (dto.PriceOverride is not null)
        {
            variant.PriceOverride = dto.PriceOverride.Value;
            hasChanges = true;
        }

        if (dto.IsActive is not null)
        {
            variant.IsActive = dto.IsActive.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/product-variants/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var variant = await _context.ProductVariants
            .FirstOrDefaultAsync(variant => variant.Id == id);

        if (variant is null)
        {
            return NotFound();
        }

        _context.ProductVariants.Remove(variant);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}