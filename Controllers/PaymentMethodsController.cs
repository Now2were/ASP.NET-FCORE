using ClothingStore.Api.Data;
using ClothingStore.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/payment-methods")]
public class PaymentMethodsController : ControllerBase
{
    private readonly EntityApiContext _context;

    public PaymentMethodsController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/payment-methods?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PaymentMethodDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PaymentMethodDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var paymentMethods = await _context.PaymentMethods
            .OrderBy(paymentMethod => paymentMethod.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return paymentMethods.Select(PaymentMethodMapper.ToDto).ToList();
    }

    // GET: api/payment-methods/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PaymentMethodDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaymentMethodDto>> GetById(int id)
    {
        var paymentMethod = await _context.PaymentMethods
            .FirstOrDefaultAsync(paymentMethod => paymentMethod.Id == id);

        if (paymentMethod is null)
        {
            return NotFound();
        }

        return PaymentMethodMapper.ToDto(paymentMethod);
    }

    // POST: api/payment-methods
    [HttpPost]
    [ProducesResponseType(typeof(PaymentMethodDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaymentMethodDto>> Create(CreatePaymentMethodDto dto)
    {
        var paymentMethod = PaymentMethodMapper.ToEntity(dto);

        _context.PaymentMethods.Add(paymentMethod);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = paymentMethod.Id }, PaymentMethodMapper.ToDto(paymentMethod));
    }

    // PUT: api/payment-methods/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdatePaymentMethodDto dto)
    {
        var paymentMethod = await _context.PaymentMethods
            .FirstOrDefaultAsync(paymentMethod => paymentMethod.Id == id);

        if (paymentMethod is null)
        {
            return NotFound();
        }

        paymentMethod.Code = dto.Code;
        paymentMethod.Name = dto.Name;
        paymentMethod.SortOrder = dto.SortOrder;
        paymentMethod.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/payment-methods/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchPaymentMethodDto dto)
    {
        var paymentMethod = await _context.PaymentMethods
            .FirstOrDefaultAsync(paymentMethod => paymentMethod.Id == id);

        if (paymentMethod is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.Code is not null)
        {
            paymentMethod.Code = dto.Code;
            hasChanges = true;
        }

        if (dto.Name is not null)
        {
            paymentMethod.Name = dto.Name;
            hasChanges = true;
        }

        if (dto.SortOrder is not null)
        {
            paymentMethod.SortOrder = dto.SortOrder.Value;
            hasChanges = true;
        }

        if (dto.IsActive is not null)
        {
            paymentMethod.IsActive = dto.IsActive.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/payment-methods/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var paymentMethod = await _context.PaymentMethods
            .FirstOrDefaultAsync(paymentMethod => paymentMethod.Id == id);

        if (paymentMethod is null)
        {
            return NotFound();
        }

        _context.PaymentMethods.Remove(paymentMethod);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}