using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.Dtos;

/// <summary>ProductMaterial: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class ProductMaterialDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string MaterialName { get; set; } = null!;
    public byte? Percent { get; set; }
    public int SortOrder { get; set; }
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
public class PatchProductMaterialDto
{
    public int? ProductId { get; set; }
    public string? MaterialName { get; set; }
    [Range(1, 100)]
    public byte? Percent { get; set; }
    public int? SortOrder { get; set; }
}

/// <summary>Статический маппер ProductMaterial -> DTO.</summary>
public static class ProductMaterialMapper
{
    public static ProductMaterialDto ToDto(ProductMaterial material) => new()
    {
        Id = material.Id,
        ProductId = material.ProductId,
        MaterialName = material.MaterialName,
        Percent = material.Percent,
        SortOrder = material.SortOrder,
    };

    public static ProductMaterial ToEntity(CreateProductMaterialDto dto) => new()
    {
        ProductId = dto.ProductId,
        MaterialName = dto.MaterialName,
        Percent = dto.Percent,
        SortOrder = dto.SortOrder,
    };
}
