using ClothingStore.Api.Data;
using ClothingStore.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/payment-statuses")]
public class PaymentStatusesController : ControllerBase
{
    private readonly EntityApiContext _context;

    public PaymentStatusesController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/payment-statuses?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PaymentStatusDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PaymentStatusDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var paymentStatuss = await _context.PaymentStatuses
            .OrderBy(paymentStatus => paymentStatus.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return paymentStatuss.Select(PaymentStatusMapper.ToDto).ToList();
    }

    // GET: api/payment-statuses/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PaymentStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaymentStatusDto>> GetById(int id)
    {
        var paymentStatus = await _context.PaymentStatuses
            .FirstOrDefaultAsync(paymentStatus => paymentStatus.Id == id);

        if (paymentStatus is null)
        {
            return NotFound();
        }

        return PaymentStatusMapper.ToDto(paymentStatus);
    }

    // POST: api/payment-statuses
    [HttpPost]
    [ProducesResponseType(typeof(PaymentStatusDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaymentStatusDto>> Create(CreatePaymentStatusDto dto)
    {
        var paymentStatus = PaymentStatusMapper.ToEntity(dto);

        _context.PaymentStatuses.Add(paymentStatus);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = paymentStatus.Id }, PaymentStatusMapper.ToDto(paymentStatus));
    }

    // PUT: api/payment-statuses/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdatePaymentStatusDto dto)
    {
        var paymentStatus = await _context.PaymentStatuses
            .FirstOrDefaultAsync(paymentStatus => paymentStatus.Id == id);

        if (paymentStatus is null)
        {
            return NotFound();
        }

        paymentStatus.Code = dto.Code;
        paymentStatus.Name = dto.Name;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/payment-statuses/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchPaymentStatusDto dto)
    {
        var paymentStatus = await _context.PaymentStatuses
            .FirstOrDefaultAsync(paymentStatus => paymentStatus.Id == id);

        if (paymentStatus is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.Code is not null)
        {
            paymentStatus.Code = dto.Code;
            hasChanges = true;
        }

        if (dto.Name is not null)
        {
            paymentStatus.Name = dto.Name;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/payment-statuses/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var paymentStatus = await _context.PaymentStatuses
            .FirstOrDefaultAsync(paymentStatus => paymentStatus.Id == id);

        if (paymentStatus is null)
        {
            return NotFound();
        }

        _context.PaymentStatuses.Remove(paymentStatus);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}