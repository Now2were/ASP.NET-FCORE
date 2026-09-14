using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.Dtos;

/// <summary>Payment: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class PaymentDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int MethodId { get; set; }
    public int StatusId { get; set; }
    public decimal Amount { get; set; }
    public string CurrencyCode { get; set; } = null!;
    public string? ExternalTransactionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PaidAt { get; set; }
}

/// <summary>Данные для создания Payment (Id и CreatedAt формируются на сервере).</summary>
public class CreatePaymentDto
{
    public int OrderId { get; set; }
    public int MethodId { get; set; }
    public int StatusId { get; set; }
    [Range(0.01, 99999999.99)]
    public decimal Amount { get; set; }
    [Required]
    public string CurrencyCode { get; set; } = null!;
    public string? ExternalTransactionId { get; set; }
    public DateTime? PaidAt { get; set; }
}

/// <summary>Полное обновление Payment: все поля обязательные.</summary>
public class UpdatePaymentDto
{
    public int OrderId { get; set; }
    public int MethodId { get; set; }
    public int StatusId { get; set; }
    [Range(0.01, 99999999.99)]
    public decimal Amount { get; set; }
    [Required]
    public string CurrencyCode { get; set; } = null!;
    public string? ExternalTransactionId { get; set; }
    public DateTime? PaidAt { get; set; }
}

/// <summary>Частичное обновление Payment: передаются только изменяемые поля (merge patch).</summary>
public class PatchPaymentDto
{
    public int? OrderId { get; set; }
    public int? MethodId { get; set; }
    public int? StatusId { get; set; }
    [Range(0.01, 99999999.99)]
    public decimal? Amount { get; set; }
    public string? CurrencyCode { get; set; }
    public string? ExternalTransactionId { get; set; }
    public DateTime? PaidAt { get; set; }
}

/// <summary>Статический маппер Payment -> DTO.</summary>
public static class PaymentMapper
{
    public static PaymentDto ToDto(Payment payment) => new()
    {
        Id = payment.Id,
        OrderId = payment.OrderId,
        MethodId = payment.MethodId,
        StatusId = payment.StatusId,
        Amount = payment.Amount,
        CurrencyCode = payment.CurrencyCode,
        ExternalTransactionId = payment.ExternalTransactionId,
        CreatedAt = payment.CreatedAt,
        PaidAt = payment.PaidAt,
    };

    public static Payment ToEntity(CreatePaymentDto dto) => new()
    {
        OrderId = dto.OrderId,
        MethodId = dto.MethodId,
        StatusId = dto.StatusId,
        Amount = dto.Amount,
        CurrencyCode = dto.CurrencyCode,
        ExternalTransactionId = dto.ExternalTransactionId,
        PaidAt = dto.PaidAt,
    };
}
