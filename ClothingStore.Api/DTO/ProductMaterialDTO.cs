using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>ProductMaterial: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class ProductMaterialDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string MaterialName { get; set; } = null!;
    public byte? Percent { get; set; }
    public int SortOrder { get; set; }
    public ProductSummaryDto? Product { get; set; }
}

/// <summary>Данные для создания ProductMaterial (Id и CreatedAt формируются на сервере).</summary>
public class CreateProductMaterialDto
{
    public int ProductId { get; set; }
    [Required]
    public string MaterialName { get; set; } = null!;
    [Range(1, 100)]
    public byte? Percent { get; set; }
    public int SortOrder { get; set; }
}

/// <summary>Полное обновление ProductMaterial: все поля обязательные.</summary>
public class UpdateProductMaterialDto
{
    public int ProductId { get; set; }
    [Required]
    public string MaterialName { get; set; } = null!;
    [Range(1, 100)]
    public byte? Percent { get; set; }
    public int SortOrder { get; set; }
}

/// <summary>Частичное обновление ProductMaterial: передаются только изменяемые поля (merge patch).</summary>
public class PatchProductMaterialDto : PatchDto
{
    private int? _productId;
    public int? ProductId { get => _productId; set => Set(ref _productId, value, nameof(ProductId)); }
    private string? _materialName;
    public string? MaterialName { get => _materialName; set => Set(ref _materialName, value, nameof(MaterialName)); }
    private byte? _percent;
    [Range(1, 100)]
    public byte? Percent { get => _percent; set => Set(ref _percent, value, nameof(Percent)); }
    private int? _sortOrder;
    public int? SortOrder { get => _sortOrder; set => Set(ref _sortOrder, value, nameof(SortOrder)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class ProductMaterialSummaryDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string MaterialName { get; set; } = null!;
    public byte? Percent { get; set; }
    public int SortOrder { get; set; }
}
