using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>DeliveryMethod: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class DeliveryMethodDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Cost { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
    public List<OrderSummaryDto> Orders { get; set; } = new();
}

/// <summary>Данные для создания DeliveryMethod (Id и CreatedAt формируются на сервере).</summary>
public class CreateDeliveryMethodDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    [Range(0, 99999999.99)]
    public decimal Cost { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Полное обновление DeliveryMethod: все поля обязательные.</summary>
public class UpdateDeliveryMethodDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    [Range(0, 99999999.99)]
    public decimal Cost { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Частичное обновление DeliveryMethod: передаются только изменяемые поля (merge patch).</summary>
public class PatchDeliveryMethodDto : PatchDto
{
    private string? _code;
    public string? Code { get => _code; set => Set(ref _code, value, nameof(Code)); }
    private string? _name;
    public string? Name { get => _name; set => Set(ref _name, value, nameof(Name)); }
    private decimal? _cost;
    [Range(0, 99999999.99)]
    public decimal? Cost { get => _cost; set => Set(ref _cost, value, nameof(Cost)); }
    private int? _sortOrder;
    public int? SortOrder { get => _sortOrder; set => Set(ref _sortOrder, value, nameof(SortOrder)); }
    private bool? _isActive;
    public bool? IsActive { get => _isActive; set => Set(ref _isActive, value, nameof(IsActive)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class DeliveryMethodSummaryDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Cost { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}
