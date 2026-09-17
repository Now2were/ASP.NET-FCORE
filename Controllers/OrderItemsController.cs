using ClothingStore.Api.Data;
using ClothingStore.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/order-items")]
public class OrderItemsController : ControllerBase
{
    private readonly EntityApiContext _context;

    public OrderItemsController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/order-items?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var orderItems = await _context.OrderItems
            .OrderBy(orderItem => orderItem.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return orderItems.Select(OrderItemMapper.ToDto).ToList();
    }

    // GET: api/order-items/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OrderItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderItemDto>> GetById(int id)
    {
        var orderItem = await _context.OrderItems
            .FirstOrDefaultAsync(orderItem => orderItem.Id == id);

        if (orderItem is null)
        {
            return NotFound();
        }

        return OrderItemMapper.ToDto(orderItem);
    }

    // POST: api/order-items
    [HttpPost]
    [ProducesResponseType(typeof(OrderItemDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderItemDto>> Create(CreateOrderItemDto dto)
    {
        var orderItem = OrderItemMapper.ToEntity(dto);

        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = orderItem.Id }, OrderItemMapper.ToDto(orderItem));
    }

    // PUT: api/order-items/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateOrderItemDto dto)
    {
        var orderItem = await _context.OrderItems
            .FirstOrDefaultAsync(orderItem => orderItem.Id == id);

        if (orderItem is null)
        {
            return NotFound();
        }

        orderItem.OrderId = dto.OrderId;
        orderItem.VariantId = dto.VariantId;
        orderItem.ProductName = dto.ProductName;
        orderItem.Sku = dto.Sku;
        orderItem.SizeName = dto.SizeName;
        orderItem.ColorName = dto.ColorName;
        orderItem.ColorHex = dto.ColorHex;
        orderItem.UnitPrice = dto.UnitPrice;
        orderItem.Quantity = dto.Quantity;
        orderItem.LineTotal = dto.LineTotal;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/order-items/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchOrderItemDto dto)
    {
        var orderItem = await _context.OrderItems
            .FirstOrDefaultAsync(orderItem => orderItem.Id == id);

        if (orderItem is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.OrderId is not null)
        {
            orderItem.OrderId = dto.OrderId.Value;
            hasChanges = true;
        }

        if (dto.VariantId is not null)
        {
            orderItem.VariantId = dto.VariantId.Value;
            hasChanges = true;
        }

        if (dto.ProductName is not null)
        {
            orderItem.ProductName = dto.ProductName;
            hasChanges = true;
        }

        if (dto.Sku is not null)
        {
            orderItem.Sku = dto.Sku;
            hasChanges = true;
        }

        if (dto.SizeName is not null)
        {
            orderItem.SizeName = dto.SizeName;
            hasChanges = true;
        }

        if (dto.ColorName is not null)
        {
            orderItem.ColorName = dto.ColorName;
            hasChanges = true;
        }

        if (dto.ColorHex is not null)
        {
            orderItem.ColorHex = dto.ColorHex;
            hasChanges = true;
        }

        if (dto.UnitPrice is not null)
        {
            orderItem.UnitPrice = dto.UnitPrice.Value;
            hasChanges = true;
        }

        if (dto.Quantity is not null)
        {
            orderItem.Quantity = dto.Quantity.Value;
            hasChanges = true;
        }

        if (dto.LineTotal is not null)
        {
            orderItem.LineTotal = dto.LineTotal.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/order-items/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var orderItem = await _context.OrderItems
            .FirstOrDefaultAsync(orderItem => orderItem.Id == id);

        if (orderItem is null)
        {
            return NotFound();
        }

        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}