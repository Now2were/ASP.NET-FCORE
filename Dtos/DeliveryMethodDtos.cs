using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.Dtos;

/// <summary>DeliveryMethod: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class DeliveryMethodDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Cost { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Данные для создания DeliveryMethod (Id и CreatedAt формируются на сервере).</summary>
public class CreateDeliveryMethodDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    [Range(0, 99999999.99)]
    public decimal Cost { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Полное обновление DeliveryMethod: все поля обязательные.</summary>
public class UpdateDeliveryMethodDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    [Range(0, 99999999.99)]
    public decimal Cost { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Частичное обновление DeliveryMethod: передаются только изменяемые поля (merge patch).</summary>
public class PatchDeliveryMethodDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    [Range(0, 99999999.99)]
    public decimal? Cost { get; set; }
    public int? SortOrder { get; set; }
    public bool? IsActive { get; set; }
}

/// <summary>Статический маппер DeliveryMethod -> DTO.</summary>
public static class DeliveryMethodMapper
{
    public static DeliveryMethodDto ToDto(DeliveryMethod deliveryMethod) => new()
    {
        Id = deliveryMethod.Id,
        Code = deliveryMethod.Code,
        Name = deliveryMethod.Name,
        Cost = deliveryMethod.Cost,
        SortOrder = deliveryMethod.SortOrder,
        IsActive = deliveryMethod.IsActive,
    };

    public static DeliveryMethod ToEntity(CreateDeliveryMethodDto dto) => new()
    {
        Code = dto.Code,
        Name = dto.Name,
        Cost = dto.Cost,
        SortOrder = dto.SortOrder,
        IsActive = dto.IsActive,
    };
}
