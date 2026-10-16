using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>ProductVariant: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class ProductVariantDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int SizeId { get; set; }
    public int ColorId { get; set; }
    public string Sku { get; set; } = null!;
    public int StockQuantity { get; set; }
    public int ReservedQuantity { get; set; }
    public decimal? PriceOverride { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<CartItemSummaryDto> CartItems { get; set; } = new();
    public ColorSummaryDto? Color { get; set; }
    public List<OrderItemSummaryDto> OrderItems { get; set; } = new();
    public ProductSummaryDto? Product { get; set; }
    public SizeSummaryDto? Size { get; set; }
    public int AvailableQuantity { get; set; }
}

/// <summary>Данные для создания ProductVariant (Id и CreatedAt формируются на сервере).</summary>
public class CreateProductVariantDto
{
    public int ProductId { get; set; }
    public int SizeId { get; set; }
    public int ColorId { get; set; }
    [Required]
    public string Sku { get; set; } = null!;
    [Range(0, 2147483647)]
    public int StockQuantity { get; set; }
    public decimal? PriceOverride { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Полное обновление ProductVariant: все поля обязательные.</summary>
public class UpdateProductVariantDto
{
    public int ProductId { get; set; }
    public int SizeId { get; set; }
    public int ColorId { get; set; }
    [Required]
    public string Sku { get; set; } = null!;
    [Range(0, 2147483647)]
    public int StockQuantity { get; set; }
    public decimal? PriceOverride { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Частичное обновление ProductVariant: передаются только изменяемые поля (merge patch).</summary>
public class PatchProductVariantDto : PatchDto
{
    private int? _productId;
    public int? ProductId { get => _productId; set => Set(ref _productId, value, nameof(ProductId)); }
    private int? _sizeId;
    public int? SizeId { get => _sizeId; set => Set(ref _sizeId, value, nameof(SizeId)); }
    private int? _colorId;
    public int? ColorId { get => _colorId; set => Set(ref _colorId, value, nameof(ColorId)); }
    private string? _sku;
    public string? Sku { get => _sku; set => Set(ref _sku, value, nameof(Sku)); }
    private int? _stockQuantity;
    [Range(0, 2147483647)]
    public int? StockQuantity { get => _stockQuantity; set => Set(ref _stockQuantity, value, nameof(StockQuantity)); }
    private decimal? _priceOverride;
    public decimal? PriceOverride { get => _priceOverride; set => Set(ref _priceOverride, value, nameof(PriceOverride)); }
    private bool? _isActive;
    public bool? IsActive { get => _isActive; set => Set(ref _isActive, value, nameof(IsActive)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class ProductVariantSummaryDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int SizeId { get; set; }
    public int ColorId { get; set; }
    public string Sku { get; set; } = null!;
    public int StockQuantity { get; set; }
    public int ReservedQuantity { get; set; }
    public decimal? PriceOverride { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
