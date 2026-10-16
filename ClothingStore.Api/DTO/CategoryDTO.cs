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
    public List<CategorySummaryDto> InverseParentCategory { get; set; } = new();
    public CategorySummaryDto? ParentCategory { get; set; }
    public List<ProductSummaryDto> Products { get; set; } = new();
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
public class PatchCategoryDto : PatchDto
{
    private int? _parentCategoryId;
    public int? ParentCategoryId { get => _parentCategoryId; set => Set(ref _parentCategoryId, value, nameof(ParentCategoryId)); }
    private string? _name;
    public string? Name { get => _name; set => Set(ref _name, value, nameof(Name)); }
    private string? _slug;
    public string? Slug { get => _slug; set => Set(ref _slug, value, nameof(Slug)); }
    private int? _sortOrder;
    public int? SortOrder { get => _sortOrder; set => Set(ref _sortOrder, value, nameof(SortOrder)); }
    private bool? _isActive;
    public bool? IsActive { get => _isActive; set => Set(ref _isActive, value, nameof(IsActive)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class CategorySummaryDto
{
    public int Id { get; set; }
    public int? ParentCategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}
