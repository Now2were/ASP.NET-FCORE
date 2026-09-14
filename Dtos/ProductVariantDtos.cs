using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.Dtos;

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
    [Range(0, 2147483647)]
    public int ReservedQuantity { get; set; }
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
    [Range(0, 2147483647)]
    public int ReservedQuantity { get; set; }
    public decimal? PriceOverride { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Частичное обновление ProductVariant: передаются только изменяемые поля (merge patch).</summary>
public class PatchProductVariantDto
{
    public int? ProductId { get; set; }
    public int? SizeId { get; set; }
    public int? ColorId { get; set; }
    public string? Sku { get; set; }
    [Range(0, 2147483647)]
    public int? StockQuantity { get; set; }
    [Range(0, 2147483647)]
    public int? ReservedQuantity { get; set; }
    public decimal? PriceOverride { get; set; }
    public bool? IsActive { get; set; }
}

/// <summary>Статический маппер ProductVariant -> DTO.</summary>
public static class ProductVariantMapper
{
    public static ProductVariantDto ToDto(ProductVariant variant) => new()
    {
        Id = variant.Id,
        ProductId = variant.ProductId,
        SizeId = variant.SizeId,
        ColorId = variant.ColorId,
        Sku = variant.Sku,
        StockQuantity = variant.StockQuantity,
        ReservedQuantity = variant.ReservedQuantity,
        PriceOverride = variant.PriceOverride,
        IsActive = variant.IsActive,
        CreatedAt = variant.CreatedAt,
    };

    public static ProductVariant ToEntity(CreateProductVariantDto dto) => new()
    {
        ProductId = dto.ProductId,
        SizeId = dto.SizeId,
        ColorId = dto.ColorId,
        Sku = dto.Sku,
        StockQuantity = dto.StockQuantity,
        ReservedQuantity = dto.ReservedQuantity,
        PriceOverride = dto.PriceOverride,
        IsActive = dto.IsActive,
    };
}
