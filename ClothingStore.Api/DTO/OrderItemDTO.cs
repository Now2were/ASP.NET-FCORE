using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>OrderItem: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class OrderItemDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int? VariantId { get; set; }
    public string ProductName { get; set; } = null!;
    public string Sku { get; set; } = null!;
    public string? SizeName { get; set; }
    public string? ColorName { get; set; }
    public string? ColorHex { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
    public OrderSummaryDto? Order { get; set; }
    public ProductVariantSummaryDto? Variant { get; set; }
}

/// <summary>Данные для создания OrderItem (Id и CreatedAt формируются на сервере).</summary>
public class CreateOrderItemDto
{
    public int OrderId { get; set; }
    public int? VariantId { get; set; }
    [Required]
    public string ProductName { get; set; } = null!;
    [Required]
    public string Sku { get; set; } = null!;
    public string? SizeName { get; set; }
    public string? ColorName { get; set; }
    public string? ColorHex { get; set; }
    [Range(0, 99999999.99)]
    public decimal UnitPrice { get; set; }
    [Range(1, 2147483647)]
    public int Quantity { get; set; }
    [Range(0, 99999999.99)]
    public decimal LineTotal { get; set; }
}

/// <summary>Полное обновление OrderItem: все поля обязательные.</summary>
public class UpdateOrderItemDto
{
    public int OrderId { get; set; }
    public int? VariantId { get; set; }
    [Required]
    public string ProductName { get; set; } = null!;
    [Required]
    public string Sku { get; set; } = null!;
    public string? SizeName { get; set; }
    public string? ColorName { get; set; }
    public string? ColorHex { get; set; }
    [Range(0, 99999999.99)]
    public decimal UnitPrice { get; set; }
    [Range(1, 2147483647)]
    public int Quantity { get; set; }
    [Range(0, 99999999.99)]
    public decimal LineTotal { get; set; }
}

/// <summary>Частичное обновление OrderItem: передаются только изменяемые поля (merge patch).</summary>
public class PatchOrderItemDto : PatchDto
{
    private int? _orderId;
    public int? OrderId { get => _orderId; set => Set(ref _orderId, value, nameof(OrderId)); }
    private int? _variantId;
    public int? VariantId { get => _variantId; set => Set(ref _variantId, value, nameof(VariantId)); }
    private string? _productName;
    public string? ProductName { get => _productName; set => Set(ref _productName, value, nameof(ProductName)); }
    private string? _sku;
    public string? Sku { get => _sku; set => Set(ref _sku, value, nameof(Sku)); }
    private string? _sizeName;
    public string? SizeName { get => _sizeName; set => Set(ref _sizeName, value, nameof(SizeName)); }
    private string? _colorName;
    public string? ColorName { get => _colorName; set => Set(ref _colorName, value, nameof(ColorName)); }
    private string? _colorHex;
    public string? ColorHex { get => _colorHex; set => Set(ref _colorHex, value, nameof(ColorHex)); }
    private decimal? _unitPrice;
    [Range(0, 99999999.99)]
    public decimal? UnitPrice { get => _unitPrice; set => Set(ref _unitPrice, value, nameof(UnitPrice)); }
    private int? _quantity;
    [Range(1, 2147483647)]
    public int? Quantity { get => _quantity; set => Set(ref _quantity, value, nameof(Quantity)); }
    private decimal? _lineTotal;
    [Range(0, 99999999.99)]
    public decimal? LineTotal { get => _lineTotal; set => Set(ref _lineTotal, value, nameof(LineTotal)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class OrderItemSummaryDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int? VariantId { get; set; }
    public string ProductName { get; set; } = null!;
    public string Sku { get; set; } = null!;
    public string? SizeName { get; set; }
    public string? ColorName { get; set; }
    public string? ColorHex { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
}
