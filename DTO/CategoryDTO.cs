using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>Category: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class CategoryDto
{
    public int Id { get; set; }
    public int? ParentCategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Данные для создания Category (Id и CreatedAt формируются на сервере).</summary>
public class CreateCategoryDto
{
    public int? ParentCategoryId { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Slug { get; set; } = null!;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Полное обновление Category: все поля обязательные.</summary>
public class UpdateCategoryDto
{
    public int? ParentCategoryId { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Slug { get; set; } = null!;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Частичное обновление Category: передаются только изменяемые поля (merge patch).</summary>
public class PatchCategoryDto
{
    public int? ParentCategoryId { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public int? SortOrder { get; set; }
    public bool? IsActive { get; set; }
}

/// <summary>Статический маппер Category -> DTO.</summary>
public static class CategoryMapper
{
    public static CategoryDto ToDto(Category category) => new()
    {
        Id = category.Id,
        ParentCategoryId = category.ParentCategoryId,
        Name = category.Name,
        Slug = category.Slug,
        SortOrder = category.SortOrder,
        IsActive = category.IsActive,
    };

    public static Category ToEntity(CreateCategoryDto dto) => new()
    {
        ParentCategoryId = dto.ParentCategoryId,
        Name = dto.Name,
        Slug = dto.Slug,
        SortOrder = dto.SortOrder,
        IsActive = dto.IsActive,
    };
}
