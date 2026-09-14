using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.Dtos;

/// <summary>Brand: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class BrandDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Country { get; set; }
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
}

/// <summary>Данные для создания Brand (Id и CreatedAt формируются на сервере).</summary>
public class CreateBrandDto
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Slug { get; set; } = null!;
    public string? Country { get; set; }
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
}

/// <summary>Полное обновление Brand: все поля обязательные.</summary>
public class UpdateBrandDto
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Slug { get; set; } = null!;
    public string? Country { get; set; }
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
}

/// <summary>Частичное обновление Brand: передаются только изменяемые поля (merge patch).</summary>
public class PatchBrandDto
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Country { get; set; }
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
}

/// <summary>Статический маппер Brand -> DTO.</summary>
public static class BrandMapper
{
    public static BrandDto ToDto(Brand brand) => new()
    {
        Id = brand.Id,
        Name = brand.Name,
        Slug = brand.Slug,
        Country = brand.Country,
        Description = brand.Description,
        LogoUrl = brand.LogoUrl,
    };

    public static Brand ToEntity(CreateBrandDto dto) => new()
    {
        Name = dto.Name,
        Slug = dto.Slug,
        Country = dto.Country,
        Description = dto.Description,
        LogoUrl = dto.LogoUrl,
    };
}
