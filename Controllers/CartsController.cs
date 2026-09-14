using ClothingStore.Api.Data;
using ClothingStore.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/carts")]
public class CartsController : ControllerBase
{
    private readonly EntityApiContext _context;

    public CartsController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/carts?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CartDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CartDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var carts = await _context.Carts
            .OrderBy(cart => cart.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return carts.Select(CartMapper.ToDto).ToList();
    }

    // GET: api/carts/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CartDto>> GetById(int id)
    {
        var cart = await _context.Carts
            .FirstOrDefaultAsync(cart => cart.Id == id);

        if (cart is null)
        {
            return NotFound();
        }

        return CartMapper.ToDto(cart);
    }

    // POST: api/carts
    [HttpPost]
    [ProducesResponseType(typeof(CartDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CartDto>> Create(CreateCartDto dto)
    {
        if (dto.UserId is null && dto.GuestToken is null)
        {
            return BadRequest(new { message = "Корзина должна принадлежать пользователю или гостю: заполните UserId или GuestToken." });
        }

        var cart = CartMapper.ToEntity(dto);
        cart.CreatedAt = DateTime.UtcNow;

        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = cart.Id }, CartMapper.ToDto(cart));
    }

    // PUT: api/carts/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateCartDto dto)
    {
        var cart = await _context.Carts
            .FirstOrDefaultAsync(cart => cart.Id == id);

        if (cart is null)
        {
            return NotFound();
        }

        cart.UserId = dto.UserId;
        cart.GuestToken = dto.GuestToken;
        cart.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/carts/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchCartDto dto)
    {
        var cart = await _context.Carts
            .FirstOrDefaultAsync(cart => cart.Id == id);

        if (cart is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.UserId is not null)
        {
            cart.UserId = dto.UserId.Value;
            hasChanges = true;
        }

        if (dto.GuestToken is not null)
        {
            cart.GuestToken = dto.GuestToken.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/carts/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var cart = await _context.Carts
            .FirstOrDefaultAsync(cart => cart.Id == id);

        if (cart is null)
        {
            return NotFound();
        }

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}