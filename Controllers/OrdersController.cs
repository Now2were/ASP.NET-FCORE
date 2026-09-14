using ClothingStore.Api.Data;
using ClothingStore.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly EntityApiContext _context;

    public OrdersController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/orders?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var orders = await _context.Orders
            .OrderBy(order => order.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return orders.Select(OrderMapper.ToDto).ToList();
    }

    // GET: api/orders/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(order => order.Id == id);

        if (order is null)
        {
            return NotFound();
        }

        return OrderMapper.ToDto(order);
    }

    // POST: api/orders
    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderDto>> Create(CreateOrderDto dto)
    {
        var order = OrderMapper.ToEntity(dto);
        order.CreatedAt = DateTime.UtcNow;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = order.Id }, OrderMapper.ToDto(order));
    }

    // PUT: api/orders/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateOrderDto dto)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(order => order.Id == id);

        if (order is null)
        {
            return NotFound();
        }

        order.OrderNumber = dto.OrderNumber;
        order.UserId = dto.UserId;
        order.StatusId = dto.StatusId;
        order.Subtotal = dto.Subtotal;
        order.DiscountAmount = dto.DiscountAmount;
        order.DeliveryCost = dto.DeliveryCost;
        order.Total = dto.Total;
        order.CurrencyCode = dto.CurrencyCode;
        order.DeliveryMethodId = dto.DeliveryMethodId;
        order.RecipientName = dto.RecipientName;
        order.RecipientPhone = dto.RecipientPhone;
        order.RecipientEmail = dto.RecipientEmail;
        order.Country = dto.Country;
        order.Region = dto.Region;
        order.City = dto.City;
        order.Street = dto.Street;
        order.House = dto.House;
        order.Apartment = dto.Apartment;
        order.PostIndex = dto.PostIndex;
        order.TrackingNumber = dto.TrackingNumber;
        order.Comment = dto.Comment;
        order.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/orders/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchOrderDto dto)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(order => order.Id == id);

        if (order is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.OrderNumber is not null)
        {
            order.OrderNumber = dto.OrderNumber;
            hasChanges = true;
        }

        if (dto.UserId is not null)
        {
            order.UserId = dto.UserId.Value;
            hasChanges = true;
        }

        if (dto.StatusId is not null)
        {
            order.StatusId = dto.StatusId.Value;
            hasChanges = true;
        }

        if (dto.Subtotal is not null)
        {
            order.Subtotal = dto.Subtotal.Value;
            hasChanges = true;
        }

        if (dto.DiscountAmount is not null)
        {
            order.DiscountAmount = dto.DiscountAmount.Value;
            hasChanges = true;
        }

        if (dto.DeliveryCost is not null)
        {
            order.DeliveryCost = dto.DeliveryCost.Value;
            hasChanges = true;
        }

        if (dto.Total is not null)
        {
            order.Total = dto.Total.Value;
            hasChanges = true;
        }

        if (dto.CurrencyCode is not null)
        {
            order.CurrencyCode = dto.CurrencyCode;
            hasChanges = true;
        }

        if (dto.DeliveryMethodId is not null)
        {
            order.DeliveryMethodId = dto.DeliveryMethodId.Value;
            hasChanges = true;
        }

        if (dto.RecipientName is not null)
        {
            order.RecipientName = dto.RecipientName;
            hasChanges = true;
        }

        if (dto.RecipientPhone is not null)
        {
            order.RecipientPhone = dto.RecipientPhone;
            hasChanges = true;
        }

        if (dto.RecipientEmail is not null)
        {
            order.RecipientEmail = dto.RecipientEmail;
            hasChanges = true;
        }

        if (dto.Country is not null)
        {
            order.Country = dto.Country;
            hasChanges = true;
        }

        if (dto.Region is not null)
        {
            order.Region = dto.Region;
            hasChanges = true;
        }

        if (dto.City is not null)
        {
            order.City = dto.City;
            hasChanges = true;
        }

        if (dto.Street is not null)
        {
            order.Street = dto.Street;
            hasChanges = true;
        }

        if (dto.House is not null)
        {
            order.House = dto.House;
            hasChanges = true;
        }

        if (dto.Apartment is not null)
        {
            order.Apartment = dto.Apartment;
            hasChanges = true;
        }

        if (dto.PostIndex is not null)
        {
            order.PostIndex = dto.PostIndex;
            hasChanges = true;
        }

        if (dto.TrackingNumber is not null)
        {
            order.TrackingNumber = dto.TrackingNumber;
            hasChanges = true;
        }

        if (dto.Comment is not null)
        {
            order.Comment = dto.Comment;
            hasChanges = true;
        }

        if (hasChanges)
        {
            order.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/orders/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(order => order.Id == id);

        if (order is null)
        {
            return NotFound();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}