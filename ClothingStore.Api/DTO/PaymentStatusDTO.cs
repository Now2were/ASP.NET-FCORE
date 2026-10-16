using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>PaymentStatus: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class PaymentStatusDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public List<PaymentSummaryDto> Payments { get; set; } = new();
}

/// <summary>Данные для создания PaymentStatus (Id и CreatedAt формируются на сервере).</summary>
public class CreatePaymentStatusDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
}

/// <summary>Полное обновление PaymentStatus: все поля обязательные.</summary>
public class UpdatePaymentStatusDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
}

/// <summary>Частичное обновление PaymentStatus: передаются только изменяемые поля (merge patch).</summary>
public class PatchPaymentStatusDto : PatchDto
{
    private string? _code;
    public string? Code { get => _code; set => Set(ref _code, value, nameof(Code)); }
    private string? _name;
    public string? Name { get => _name; set => Set(ref _name, value, nameof(Name)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class PaymentStatusSummaryDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
}
