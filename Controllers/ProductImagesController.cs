using ClothingStore.Api.Data;
using ClothingStore.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/product-images")]
public class ProductImagesController : ControllerBase
{
    private readonly EntityApiContext _context;

    public ProductImagesController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/product-images?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductImageDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ProductImageDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var images = await _context.ProductImages
            .OrderBy(image => image.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return images.Select(ProductImageMapper.ToDto).ToList();
    }

    // GET: api/product-images/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductImageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductImageDto>> GetById(int id)
    {
        var image = await _context.ProductImages
            .FirstOrDefaultAsync(image => image.Id == id);

        if (image is null)
        {
            return NotFound();
        }

        return ProductImageMapper.ToDto(image);
    }

    // POST: api/product-images
    [HttpPost]
    [ProducesResponseType(typeof(ProductImageDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductImageDto>> Create(CreateProductImageDto dto)
    {
        var image = ProductImageMapper.ToEntity(dto);

        _context.ProductImages.Add(image);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = image.Id }, ProductImageMapper.ToDto(image));
    }

    // PUT: api/product-images/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateProductImageDto dto)
    {
        var image = await _context.ProductImages
            .FirstOrDefaultAsync(image => image.Id == id);

        if (image is null)
        {
            return NotFound();
        }

        image.ProductId = dto.ProductId;
        image.ImageUrl = dto.ImageUrl;
        image.AltText = dto.AltText;
        image.SortOrder = dto.SortOrder;
        image.IsMain = dto.IsMain;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/product-images/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchProductImageDto dto)
    {
        var image = await _context.ProductImages
            .FirstOrDefaultAsync(image => image.Id == id);

        if (image is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.ProductId is not null)
        {
            image.ProductId = dto.ProductId.Value;
            hasChanges = true;
        }

        if (dto.ImageUrl is not null)
        {
            image.ImageUrl = dto.ImageUrl;
            hasChanges = true;
        }

        if (dto.AltText is not null)
        {
            image.AltText = dto.AltText;
            hasChanges = true;
        }

        if (dto.SortOrder is not null)
        {
            image.SortOrder = dto.SortOrder.Value;
            hasChanges = true;
        }

        if (dto.IsMain is not null)
        {
            image.IsMain = dto.IsMain.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/product-images/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var image = await _context.ProductImages
            .FirstOrDefaultAsync(image => image.Id == id);

        if (image is null)
        {
            return NotFound();
        }

        _context.ProductImages.Remove(image);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}