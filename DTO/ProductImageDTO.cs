using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>ProductImage: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class ProductImageDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string? AltText { get; set; }
    public int SortOrder { get; set; }
    public bool IsMain { get; set; }
}

/// <summary>Данные для создания ProductImage (Id и CreatedAt формируются на сервере).</summary>
public class CreateProductImageDto
{
    public int ProductId { get; set; }
    [Required]
    public string ImageUrl { get; set; } = null!;
    public string? AltText { get; set; }
    public int SortOrder { get; set; }
    public bool IsMain { get; set; }
}

/// <summary>Полное обновление ProductImage: все поля обязательные.</summary>
public class UpdateProductImageDto
{
    public int ProductId { get; set; }
    [Required]
    public string ImageUrl { get; set; } = null!;
    public string? AltText { get; set; }
    public int SortOrder { get; set; }
    public bool IsMain { get; set; }
}

/// <summary>Частичное обновление ProductImage: передаются только изменяемые поля (merge patch).</summary>
public class PatchProductImageDto
{
    public int? ProductId { get; set; }
    public string? ImageUrl { get; set; }
    public string? AltText { get; set; }
    public int? SortOrder { get; set; }
    public bool? IsMain { get; set; }
}

/// <summary>Статический маппер ProductImage -> DTO.</summary>
public static class ProductImageMapper
{
    public static ProductImageDto ToDto(ProductImage image) => new()
    {
        Id = image.Id,
        ProductId = image.ProductId,
        ImageUrl = image.ImageUrl,
        AltText = image.AltText,
        SortOrder = image.SortOrder,
        IsMain = image.IsMain,
    };

    public static ProductImage ToEntity(CreateProductImageDto dto) => new()
    {
        ProductId = dto.ProductId,
        ImageUrl = dto.ImageUrl,
        AltText = dto.AltText,
        SortOrder = dto.SortOrder,
        IsMain = dto.IsMain,
    };
}
