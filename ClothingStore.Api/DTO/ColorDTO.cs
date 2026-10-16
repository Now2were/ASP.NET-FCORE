using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>Color: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class ColorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Hex { get; set; } = null!;
    public List<ProductVariantSummaryDto> ProductVariants { get; set; } = new();
}

/// <summary>Данные для создания Color (Id и CreatedAt формируются на сервере).</summary>
public class CreateColorDto
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Hex { get; set; } = null!;
}

/// <summary>Полное обновление Color: все поля обязательные.</summary>
public class UpdateColorDto
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Hex { get; set; } = null!;
}

/// <summary>Частичное обновление Color: передаются только изменяемые поля (merge patch).</summary>
public class PatchColorDto : PatchDto
{
    private string? _name;
    public string? Name { get => _name; set => Set(ref _name, value, nameof(Name)); }
    private string? _code;
    public string? Code { get => _code; set => Set(ref _code, value, nameof(Code)); }
    private string? _hex;
    public string? Hex { get => _hex; set => Set(ref _hex, value, nameof(Hex)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class ColorSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Hex { get; set; } = null!;
}
