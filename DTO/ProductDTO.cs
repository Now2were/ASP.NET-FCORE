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
public class PatchProductDto
{
    public int? CategoryId { get; set; }
    public int? BrandId { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Description { get; set; }
    public byte? Gender { get; set; }
    [Range(0.01, 99999999.99)]
    public decimal? Price { get; set; }
    [Range(0.01, 99999999.99)]
    public decimal? SalePrice { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsBestseller { get; set; }
    [Range(0, 9.99)]
    public decimal? AverageRating { get; set; }
    public int? RatingCount { get; set; }
}

/// <summary>Статический маппер Product -> DTO.</summary>
public static class ProductMapper
{
    public static ProductDto ToDto(Product product) => new()
    {
        Id = product.Id,
        CategoryId = product.CategoryId,
        BrandId = product.BrandId,
        Name = product.Name,
        Slug = product.Slug,
        Description = product.Description,
        Gender = product.Gender,
        Price = product.Price,
        SalePrice = product.SalePrice,
        IsActive = product.IsActive,
        IsBestseller = product.IsBestseller,
        AverageRating = product.AverageRating,
        RatingCount = product.RatingCount,
        CreatedAt = product.CreatedAt,
        UpdatedAt = product.UpdatedAt,
    };

    public static Product ToEntity(CreateProductDto dto) => new()
    {
        CategoryId = dto.CategoryId,
        BrandId = dto.BrandId,
        Name = dto.Name,
        Slug = dto.Slug,
        Description = dto.Description,
        Gender = dto.Gender,
        Price = dto.Price,
        SalePrice = dto.SalePrice,
        IsActive = dto.IsActive,
        IsBestseller = dto.IsBestseller,
        AverageRating = dto.AverageRating,
        RatingCount = dto.RatingCount,
    };
}
