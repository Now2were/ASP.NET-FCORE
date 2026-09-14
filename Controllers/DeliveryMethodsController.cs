using ClothingStore.Api.Data;
using ClothingStore.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/delivery-methods")]
public class DeliveryMethodsController : ControllerBase
{
    private readonly EntityApiContext _context;

    public DeliveryMethodsController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/delivery-methods?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DeliveryMethodDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var deliveryMethods = await _context.DeliveryMethods
            .OrderBy(deliveryMethod => deliveryMethod.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return deliveryMethods.Select(DeliveryMethodMapper.ToDto).ToList();
    }

    // GET: api/delivery-methods/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(DeliveryMethodDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeliveryMethodDto>> GetById(int id)
    {
        var deliveryMethod = await _context.DeliveryMethods
            .FirstOrDefaultAsync(deliveryMethod => deliveryMethod.Id == id);

        if (deliveryMethod is null)
        {
            return NotFound();
        }

        return DeliveryMethodMapper.ToDto(deliveryMethod);
    }

    // POST: api/delivery-methods
    [HttpPost]
    [ProducesResponseType(typeof(DeliveryMethodDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DeliveryMethodDto>> Create(CreateDeliveryMethodDto dto)
    {
        var deliveryMethod = DeliveryMethodMapper.ToEntity(dto);

        _context.DeliveryMethods.Add(deliveryMethod);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = deliveryMethod.Id }, DeliveryMethodMapper.ToDto(deliveryMethod));
    }

    // PUT: api/delivery-methods/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateDeliveryMethodDto dto)
    {
        var deliveryMethod = await _context.DeliveryMethods
            .FirstOrDefaultAsync(deliveryMethod => deliveryMethod.Id == id);

        if (deliveryMethod is null)
        {
            return NotFound();
        }

        deliveryMethod.Code = dto.Code;
        deliveryMethod.Name = dto.Name;
        deliveryMethod.Cost = dto.Cost;
        deliveryMethod.SortOrder = dto.SortOrder;
        deliveryMethod.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/delivery-methods/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchDeliveryMethodDto dto)
    {
        var deliveryMethod = await _context.DeliveryMethods
            .FirstOrDefaultAsync(deliveryMethod => deliveryMethod.Id == id);

        if (deliveryMethod is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.Code is not null)
        {
            deliveryMethod.Code = dto.Code;
            hasChanges = true;
        }

        if (dto.Name is not null)
        {
            deliveryMethod.Name = dto.Name;
            hasChanges = true;
        }

        if (dto.Cost is not null)
        {
            deliveryMethod.Cost = dto.Cost.Value;
            hasChanges = true;
        }

        if (dto.SortOrder is not null)
        {
            deliveryMethod.SortOrder = dto.SortOrder.Value;
            hasChanges = true;
        }

        if (dto.IsActive is not null)
        {
            deliveryMethod.IsActive = dto.IsActive.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/delivery-methods/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deliveryMethod = await _context.DeliveryMethods
            .FirstOrDefaultAsync(deliveryMethod => deliveryMethod.Id == id);

        if (deliveryMethod is null)
        {
            return NotFound();
        }

        _context.DeliveryMethods.Remove(deliveryMethod);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}