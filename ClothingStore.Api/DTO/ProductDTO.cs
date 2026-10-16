using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>Product: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class ProductDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public byte Gender { get; set; }
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    public bool IsActive { get; set; }
    public bool IsBestseller { get; set; }
    public decimal AverageRating { get; set; }
    public int RatingCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public BrandSummaryDto? Brand { get; set; }
    public CategorySummaryDto? Category { get; set; }
    public List<ProductImageSummaryDto> ProductImages { get; set; } = new();
    public List<ProductMaterialSummaryDto> ProductMaterials { get; set; } = new();
    public List<ProductVariantSummaryDto> ProductVariants { get; set; } = new();
    public List<ReviewSummaryDto> Reviews { get; set; } = new();
}

/// <summary>Данные для создания Product (Id и CreatedAt формируются на сервере).</summary>
public class CreateProductDto
{
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public byte Gender { get; set; }
    [Range(0.01, 99999999.99)]
    public decimal Price { get; set; }
    [Range(0.01, 99999999.99)]
    public decimal? SalePrice { get; set; }
    public bool IsActive { get; set; }
    public bool IsBestseller { get; set; }
    [Range(0, 9.99)]
    public decimal AverageRating { get; set; }
    public int RatingCount { get; set; }
}

/// <summary>Полное обновление Product: все поля обязательные.</summary>
public class UpdateProductDto
{
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public byte Gender { get; set; }
    [Range(0.01, 99999999.99)]
    public decimal Price { get; set; }
    [Range(0.01, 99999999.99)]
    public decimal? SalePrice { get; set; }
    public bool IsActive { get; set; }
    public bool IsBestseller { get; set; }
    [Range(0, 9.99)]
    public decimal AverageRating { get; set; }
    public int RatingCount { get; set; }
}

/// <summary>Частичное обновление Product: передаются только изменяемые поля (merge patch).</summary>
public class PatchProductDto : PatchDto
{
    private int? _categoryId;
    public int? CategoryId { get => _categoryId; set => Set(ref _categoryId, value, nameof(CategoryId)); }
    private int? _brandId;
    public int? BrandId { get => _brandId; set => Set(ref _brandId, value, nameof(BrandId)); }
    private string? _name;
    public string? Name { get => _name; set => Set(ref _name, value, nameof(Name)); }
    private string? _slug;
    public string? Slug { get => _slug; set => Set(ref _slug, value, nameof(Slug)); }
    private string? _description;
    public string? Description { get => _description; set => Set(ref _description, value, nameof(Description)); }
    private byte? _gender;
    public byte? Gender { get => _gender; set => Set(ref _gender, value, nameof(Gender)); }
    private decimal? _price;
    [Range(0.01, 99999999.99)]
    public decimal? Price { get => _price; set => Set(ref _price, value, nameof(Price)); }
    private decimal? _salePrice;
    [Range(0.01, 99999999.99)]
    public decimal? SalePrice { get => _salePrice; set => Set(ref _salePrice, value, nameof(SalePrice)); }
    private bool? _isActive;
    public bool? IsActive { get => _isActive; set => Set(ref _isActive, value, nameof(IsActive)); }
    private bool? _isBestseller;
    public bool? IsBestseller { get => _isBestseller; set => Set(ref _isBestseller, value, nameof(IsBestseller)); }
    private decimal? _averageRating;
    [Range(0, 9.99)]
    public decimal? AverageRating { get => _averageRating; set => Set(ref _averageRating, value, nameof(AverageRating)); }
    private int? _ratingCount;
    public int? RatingCount { get => _ratingCount; set => Set(ref _ratingCount, value, nameof(RatingCount)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class ProductSummaryDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public byte Gender { get; set; }
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    public bool IsActive { get; set; }
    public bool IsBestseller { get; set; }
    public decimal AverageRating { get; set; }
    public int RatingCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
