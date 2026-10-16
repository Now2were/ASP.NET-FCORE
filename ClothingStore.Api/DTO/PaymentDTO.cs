using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

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
    public PaymentMethodSummaryDto? Method { get; set; }
    public OrderSummaryDto? Order { get; set; }
    public PaymentStatusSummaryDto? Status { get; set; }
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
public class PatchPaymentDto : PatchDto
{
    private int? _orderId;
    public int? OrderId { get => _orderId; set => Set(ref _orderId, value, nameof(OrderId)); }
    private int? _methodId;
    public int? MethodId { get => _methodId; set => Set(ref _methodId, value, nameof(MethodId)); }
    private int? _statusId;
    public int? StatusId { get => _statusId; set => Set(ref _statusId, value, nameof(StatusId)); }
    private decimal? _amount;
    [Range(0.01, 99999999.99)]
    public decimal? Amount { get => _amount; set => Set(ref _amount, value, nameof(Amount)); }
    private string? _currencyCode;
    public string? CurrencyCode { get => _currencyCode; set => Set(ref _currencyCode, value, nameof(CurrencyCode)); }
    private string? _externalTransactionId;
    public string? ExternalTransactionId { get => _externalTransactionId; set => Set(ref _externalTransactionId, value, nameof(ExternalTransactionId)); }
    private DateTime? _paidAt;
    public DateTime? PaidAt { get => _paidAt; set => Set(ref _paidAt, value, nameof(PaidAt)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class PaymentSummaryDto
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
