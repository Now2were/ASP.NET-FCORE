using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>Size: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class SizeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
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
public class PatchSizeDto
{
    public string? Name { get; set; }
    public int? SortOrder { get; set; }
}

/// <summary>Статический маппер Size -> DTO.</summary>
public static class SizeMapper
{
    public static SizeDto ToDto(Size size) => new()
    {
        Id = size.Id,
        Name = size.Name,
        SortOrder = size.SortOrder,
    };

    public static Size ToEntity(CreateSizeDto dto) => new()
    {
        Name = dto.Name,
        SortOrder = dto.SortOrder,
    };
}
