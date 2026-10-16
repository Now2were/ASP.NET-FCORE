using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>Brand: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class BrandDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Country { get; set; }
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public List<ProductSummaryDto> Products { get; set; } = new();
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
public class PatchBrandDto : PatchDto
{
    private string? _name;
    public string? Name { get => _name; set => Set(ref _name, value, nameof(Name)); }
    private string? _slug;
    public string? Slug { get => _slug; set => Set(ref _slug, value, nameof(Slug)); }
    private string? _country;
    public string? Country { get => _country; set => Set(ref _country, value, nameof(Country)); }
    private string? _description;
    public string? Description { get => _description; set => Set(ref _description, value, nameof(Description)); }
    private string? _logoUrl;
    public string? LogoUrl { get => _logoUrl; set => Set(ref _logoUrl, value, nameof(LogoUrl)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class BrandSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Country { get; set; }
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
}
