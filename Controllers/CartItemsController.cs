using ClothingStore.Api.Data;
using ClothingStore.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/cart-items")]
public class CartItemsController : ControllerBase
{
    private readonly EntityApiContext _context;

    public CartItemsController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/cart-items?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CartItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var cartItems = await _context.CartItems
            .OrderBy(cartItem => cartItem.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return cartItems.Select(CartItemMapper.ToDto).ToList();
    }

    // GET: api/cart-items/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CartItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CartItemDto>> GetById(int id)
    {
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(cartItem => cartItem.Id == id);

        if (cartItem is null)
        {
            return NotFound();
        }

        return CartItemMapper.ToDto(cartItem);
    }

    // POST: api/cart-items
    [HttpPost]
    [ProducesResponseType(typeof(CartItemDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CartItemDto>> Create(CreateCartItemDto dto)
    {
        var cartItem = CartItemMapper.ToEntity(dto);
        cartItem.CreatedAt = DateTime.UtcNow;

        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = cartItem.Id }, CartItemMapper.ToDto(cartItem));
    }

    // PUT: api/cart-items/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateCartItemDto dto)
    {
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(cartItem => cartItem.Id == id);

        if (cartItem is null)
        {
            return NotFound();
        }

        cartItem.CartId = dto.CartId;
        cartItem.VariantId = dto.VariantId;
        cartItem.Quantity = dto.Quantity;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/cart-items/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchCartItemDto dto)
    {
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(cartItem => cartItem.Id == id);

        if (cartItem is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.CartId is not null)
        {
            cartItem.CartId = dto.CartId.Value;
            hasChanges = true;
        }

        if (dto.VariantId is not null)
        {
            cartItem.VariantId = dto.VariantId.Value;
            hasChanges = true;
        }

        if (dto.Quantity is not null)
        {
            cartItem.Quantity = dto.Quantity.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/cart-items/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(cartItem => cartItem.Id == id);

        if (cartItem is null)
        {
            return NotFound();
        }

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}