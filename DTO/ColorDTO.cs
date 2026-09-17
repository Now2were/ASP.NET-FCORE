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
public class PatchColorDto
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Hex { get; set; }
}

/// <summary>Статический маппер Color -> DTO.</summary>
public static class ColorMapper
{
    public static ColorDto ToDto(Color color) => new()
    {
        Id = color.Id,
        Name = color.Name,
        Code = color.Code,
        Hex = color.Hex,
    };

    public static Color ToEntity(CreateColorDto dto) => new()
    {
        Name = dto.Name,
        Code = dto.Code,
        Hex = dto.Hex,
    };
}
