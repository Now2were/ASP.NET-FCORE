using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>Size: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class SizeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
    public List<ProductVariantSummaryDto> ProductVariants { get; set; } = new();
}

/// <summary>Данные для создания Size (Id и CreatedAt формируются на сервере).</summary>
public class CreateSizeDto
{
    [Required]
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
}

/// <summary>Полное обновление Size: все поля обязательные.</summary>
public class UpdateSizeDto
{
    [Required]
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
}

/// <summary>Частичное обновление Size: передаются только изменяемые поля (merge patch).</summary>
public class PatchSizeDto : PatchDto
{
    private string? _name;
    public string? Name { get => _name; set => Set(ref _name, value, nameof(Name)); }
    private int? _sortOrder;
    public int? SortOrder { get => _sortOrder; set => Set(ref _sortOrder, value, nameof(SortOrder)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class SizeSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
}
