using ClothingStore.Api.Data;
using ClothingStore.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/order-statuses")]
public class OrderStatusesController : ControllerBase
{
    private readonly EntityApiContext _context;

    public OrderStatusesController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/order-statuses?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderStatusDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OrderStatusDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var orderStatuss = await _context.OrderStatuses
            .OrderBy(orderStatus => orderStatus.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return orderStatuss.Select(OrderStatusMapper.ToDto).ToList();
    }

    // GET: api/order-statuses/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OrderStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderStatusDto>> GetById(int id)
    {
        var orderStatus = await _context.OrderStatuses
            .FirstOrDefaultAsync(orderStatus => orderStatus.Id == id);

        if (orderStatus is null)
        {
            return NotFound();
        }

        return OrderStatusMapper.ToDto(orderStatus);
    }

    // POST: api/order-statuses
    [HttpPost]
    [ProducesResponseType(typeof(OrderStatusDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderStatusDto>> Create(CreateOrderStatusDto dto)
    {
        var orderStatus = OrderStatusMapper.ToEntity(dto);

        _context.OrderStatuses.Add(orderStatus);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = orderStatus.Id }, OrderStatusMapper.ToDto(orderStatus));
    }

    // PUT: api/order-statuses/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateOrderStatusDto dto)
    {
        var orderStatus = await _context.OrderStatuses
            .FirstOrDefaultAsync(orderStatus => orderStatus.Id == id);

        if (orderStatus is null)
        {
            return NotFound();
        }

        orderStatus.Code = dto.Code;
        orderStatus.Name = dto.Name;
        orderStatus.SortOrder = dto.SortOrder;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/order-statuses/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchOrderStatusDto dto)
    {
        var orderStatus = await _context.OrderStatuses
            .FirstOrDefaultAsync(orderStatus => orderStatus.Id == id);

        if (orderStatus is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.Code is not null)
        {
            orderStatus.Code = dto.Code;
            hasChanges = true;
        }

        if (dto.Name is not null)
        {
            orderStatus.Name = dto.Name;
            hasChanges = true;
        }

        if (dto.SortOrder is not null)
        {
            orderStatus.SortOrder = dto.SortOrder.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/order-statuses/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var orderStatus = await _context.OrderStatuses
            .FirstOrDefaultAsync(orderStatus => orderStatus.Id == id);

        if (orderStatus is null)
        {
            return NotFound();
        }

        _context.OrderStatuses.Remove(orderStatus);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}