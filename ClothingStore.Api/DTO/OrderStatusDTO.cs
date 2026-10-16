using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>OrderStatus: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class OrderStatusDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
    public List<OrderSummaryDto> Orders { get; set; } = new();
}

/// <summary>Данные для создания OrderStatus (Id и CreatedAt формируются на сервере).</summary>
public class CreateOrderStatusDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
}

/// <summary>Полное обновление OrderStatus: все поля обязательные.</summary>
public class UpdateOrderStatusDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
}

/// <summary>Частичное обновление OrderStatus: передаются только изменяемые поля (merge patch).</summary>
public class PatchOrderStatusDto : PatchDto
{
    private string? _code;
    public string? Code { get => _code; set => Set(ref _code, value, nameof(Code)); }
    private string? _name;
    public string? Name { get => _name; set => Set(ref _name, value, nameof(Name)); }
    private int? _sortOrder;
    public int? SortOrder { get => _sortOrder; set => Set(ref _sortOrder, value, nameof(SortOrder)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class OrderStatusSummaryDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
}
