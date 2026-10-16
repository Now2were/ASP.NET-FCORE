using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>PaymentMethod: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class PaymentMethodDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
    public List<PaymentSummaryDto> Payments { get; set; } = new();
}

/// <summary>Данные для создания PaymentMethod (Id и CreatedAt формируются на сервере).</summary>
public class CreatePaymentMethodDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Полное обновление PaymentMethod: все поля обязательные.</summary>
public class UpdatePaymentMethodDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Частичное обновление PaymentMethod: передаются только изменяемые поля (merge patch).</summary>
public class PatchPaymentMethodDto : PatchDto
{
    private string? _code;
    public string? Code { get => _code; set => Set(ref _code, value, nameof(Code)); }
    private string? _name;
    public string? Name { get => _name; set => Set(ref _name, value, nameof(Name)); }
    private int? _sortOrder;
    public int? SortOrder { get => _sortOrder; set => Set(ref _sortOrder, value, nameof(SortOrder)); }
    private bool? _isActive;
    public bool? IsActive { get => _isActive; set => Set(ref _isActive, value, nameof(IsActive)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class PaymentMethodSummaryDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}
