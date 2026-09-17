using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>OrderStatus: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class OrderStatusDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
}

/// <summary>Данные для создания OrderStatus (Id и CreatedAt формируются на сервере).</summary>
public class CreateOrderStatusDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
}

/// <summary>Полное обновление OrderStatus: все поля обязательные.</summary>
public class UpdateOrderStatusDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
}

/// <summary>Частичное обновление OrderStatus: передаются только изменяемые поля (merge patch).</summary>
public class PatchOrderStatusDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public int? SortOrder { get; set; }
}

/// <summary>Статический маппер OrderStatus -> DTO.</summary>
public static class OrderStatusMapper
{
    public static OrderStatusDto ToDto(OrderStatus orderStatus) => new()
    {
        Id = orderStatus.Id,
        Code = orderStatus.Code,
        Name = orderStatus.Name,
        SortOrder = orderStatus.SortOrder,
    };

    public static OrderStatus ToEntity(CreateOrderStatusDto dto) => new()
    {
        Code = dto.Code,
        Name = dto.Name,
        SortOrder = dto.SortOrder,
    };
}
