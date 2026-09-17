using ClothingStore.Api.Data;
using ClothingStore.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly EntityApiContext _context;

    public ProductsController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/products?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var products = await _context.Products
            .OrderBy(product => product.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return products.Select(ProductMapper.ToDto).ToList();
    }

    // GET: api/products/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(product => product.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        return ProductMapper.ToDto(product);
    }

    // POST: api/products
    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)
    {
        var product = ProductMapper.ToEntity(dto);
        product.CreatedAt = DateTime.UtcNow;

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, ProductMapper.ToDto(product));
    }

    // PUT: api/products/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateProductDto dto)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(product => product.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        product.CategoryId = dto.CategoryId;
        product.BrandId = dto.BrandId;
        product.Name = dto.Name;
        product.Slug = dto.Slug;
        product.Description = dto.Description;
        product.Gender = dto.Gender;
        product.Price = dto.Price;
        product.SalePrice = dto.SalePrice;
        product.IsActive = dto.IsActive;
        product.IsBestseller = dto.IsBestseller;
        product.AverageRating = dto.AverageRating;
        product.RatingCount = dto.RatingCount;
        product.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/products/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchProductDto dto)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(product => product.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.CategoryId is not null)
        {
            product.CategoryId = dto.CategoryId.Value;
            hasChanges = true;
        }

        if (dto.BrandId is not null)
        {
            product.BrandId = dto.BrandId.Value;
            hasChanges = true;
        }

        if (dto.Name is not null)
        {
            product.Name = dto.Name;
            hasChanges = true;
        }

        if (dto.Slug is not null)
        {
            product.Slug = dto.Slug;
            hasChanges = true;
        }

        if (dto.Description is not null)
        {
            product.Description = dto.Description;
            hasChanges = true;
        }

        if (dto.Gender is not null)
        {
            product.Gender = dto.Gender.Value;
            hasChanges = true;
        }

        if (dto.Price is not null)
        {
            product.Price = dto.Price.Value;
            hasChanges = true;
        }

        if (dto.SalePrice is not null)
        {
            product.SalePrice = dto.SalePrice.Value;
            hasChanges = true;
        }

        if (dto.IsActive is not null)
        {
            product.IsActive = dto.IsActive.Value;
            hasChanges = true;
        }

        if (dto.IsBestseller is not null)
        {
            product.IsBestseller = dto.IsBestseller.Value;
            hasChanges = true;
        }

        if (dto.AverageRating is not null)
        {
            product.AverageRating = dto.AverageRating.Value;
            hasChanges = true;
        }

        if (dto.RatingCount is not null)
        {
            product.RatingCount = dto.RatingCount.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            product.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/products/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(product => product.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}