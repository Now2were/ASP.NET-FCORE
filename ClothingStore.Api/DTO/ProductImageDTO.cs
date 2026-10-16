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
    public ProductSummaryDto? Product { get; set; }
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
public class PatchProductImageDto : PatchDto
{
    private int? _productId;
    public int? ProductId { get => _productId; set => Set(ref _productId, value, nameof(ProductId)); }
    private string? _imageUrl;
    public string? ImageUrl { get => _imageUrl; set => Set(ref _imageUrl, value, nameof(ImageUrl)); }
    private string? _altText;
    public string? AltText { get => _altText; set => Set(ref _altText, value, nameof(AltText)); }
    private int? _sortOrder;
    public int? SortOrder { get => _sortOrder; set => Set(ref _sortOrder, value, nameof(SortOrder)); }
    private bool? _isMain;
    public bool? IsMain { get => _isMain; set => Set(ref _isMain, value, nameof(IsMain)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class ProductImageSummaryDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string? AltText { get; set; }
    public int SortOrder { get; set; }
    public bool IsMain { get; set; }
}
