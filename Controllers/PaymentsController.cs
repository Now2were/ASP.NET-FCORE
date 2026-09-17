using ClothingStore.Api.Data;
using ClothingStore.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly EntityApiContext _context;

    public PaymentsController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/payments?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PaymentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var payments = await _context.Payments
            .OrderBy(payment => payment.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return payments.Select(PaymentMapper.ToDto).ToList();
    }

    // GET: api/payments/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PaymentDto>> GetById(int id)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(payment => payment.Id == id);

        if (payment is null)
        {
            return NotFound();
        }

        return PaymentMapper.ToDto(payment);
    }

    // POST: api/payments
    [HttpPost]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaymentDto>> Create(CreatePaymentDto dto)
    {
        var payment = PaymentMapper.ToEntity(dto);
        payment.CreatedAt = DateTime.UtcNow;

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = payment.Id }, PaymentMapper.ToDto(payment));
    }

    // PUT: api/payments/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdatePaymentDto dto)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(payment => payment.Id == id);

        if (payment is null)
        {
            return NotFound();
        }

        payment.OrderId = dto.OrderId;
        payment.MethodId = dto.MethodId;
        payment.StatusId = dto.StatusId;
        payment.Amount = dto.Amount;
        payment.CurrencyCode = dto.CurrencyCode;
        payment.ExternalTransactionId = dto.ExternalTransactionId;
        payment.PaidAt = dto.PaidAt;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/payments/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchPaymentDto dto)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(payment => payment.Id == id);

        if (payment is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.OrderId is not null)
        {
            payment.OrderId = dto.OrderId.Value;
            hasChanges = true;
        }

        if (dto.MethodId is not null)
        {
            payment.MethodId = dto.MethodId.Value;
            hasChanges = true;
        }

        if (dto.StatusId is not null)
        {
            payment.StatusId = dto.StatusId.Value;
            hasChanges = true;
        }

        if (dto.Amount is not null)
        {
            payment.Amount = dto.Amount.Value;
            hasChanges = true;
        }

        if (dto.CurrencyCode is not null)
        {
            payment.CurrencyCode = dto.CurrencyCode;
            hasChanges = true;
        }

        if (dto.ExternalTransactionId is not null)
        {
            payment.ExternalTransactionId = dto.ExternalTransactionId;
            hasChanges = true;
        }

        if (dto.PaidAt is not null)
        {
            payment.PaidAt = dto.PaidAt.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/payments/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var payment = await _context.Payments
            .FirstOrDefaultAsync(payment => payment.Id == id);

        if (payment is null)
        {
            return NotFound();
        }

        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}