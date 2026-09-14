using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.Dtos;

/// <summary>OrderItem: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class OrderItemDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int? VariantId { get; set; }
    public string ProductName { get; set; } = null!;
    public string Sku { get; set; } = null!;
    public string? SizeName { get; set; }
    public string? ColorName { get; set; }
    public string? ColorHex { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
}

/// <summary>Данные для создания OrderItem (Id и CreatedAt формируются на сервере).</summary>
public class CreateOrderItemDto
{
    public int OrderId { get; set; }
    public int? VariantId { get; set; }
    [Required]
    public string ProductName { get; set; } = null!;
    [Required]
    public string Sku { get; set; } = null!;
    public string? SizeName { get; set; }
    public string? ColorName { get; set; }
    public string? ColorHex { get; set; }
    [Range(0, 99999999.99)]
    public decimal UnitPrice { get; set; }
    [Range(1, 2147483647)]
    public int Quantity { get; set; }
    [Range(0, 99999999.99)]
    public decimal LineTotal { get; set; }
}

/// <summary>Полное обновление OrderItem: все поля обязательные.</summary>
public class UpdateOrderItemDto
{
    public int OrderId { get; set; }
    public int? VariantId { get; set; }
    [Required]
    public string ProductName { get; set; } = null!;
    [Required]
    public string Sku { get; set; } = null!;
    public string? SizeName { get; set; }
    public string? ColorName { get; set; }
    public string? ColorHex { get; set; }
    [Range(0, 99999999.99)]
    public decimal UnitPrice { get; set; }
    [Range(1, 2147483647)]
    public int Quantity { get; set; }
    [Range(0, 99999999.99)]
    public decimal LineTotal { get; set; }
}

/// <summary>Частичное обновление OrderItem: передаются только изменяемые поля (merge patch).</summary>
public class PatchOrderItemDto
{
    public int? OrderId { get; set; }
    public int? VariantId { get; set; }
    public string? ProductName { get; set; }
    public string? Sku { get; set; }
    public string? SizeName { get; set; }
    public string? ColorName { get; set; }
    public string? ColorHex { get; set; }
    [Range(0, 99999999.99)]
    public decimal? UnitPrice { get; set; }
    [Range(1, 2147483647)]
    public int? Quantity { get; set; }
    [Range(0, 99999999.99)]
    public decimal? LineTotal { get; set; }
}

/// <summary>Статический маппер OrderItem -> DTO.</summary>
public static class OrderItemMapper
{
    public static OrderItemDto ToDto(OrderItem orderItem) => new()
    {
        Id = orderItem.Id,
        OrderId = orderItem.OrderId,
        VariantId = orderItem.VariantId,
        ProductName = orderItem.ProductName,
        Sku = orderItem.Sku,
        SizeName = orderItem.SizeName,
        ColorName = orderItem.ColorName,
        ColorHex = orderItem.ColorHex,
        UnitPrice = orderItem.UnitPrice,
        Quantity = orderItem.Quantity,
        LineTotal = orderItem.LineTotal,
    };

    public static OrderItem ToEntity(CreateOrderItemDto dto) => new()
    {
        OrderId = dto.OrderId,
        VariantId = dto.VariantId,
        ProductName = dto.ProductName,
        Sku = dto.Sku,
        SizeName = dto.SizeName,
        ColorName = dto.ColorName,
        ColorHex = dto.ColorHex,
        UnitPrice = dto.UnitPrice,
        Quantity = dto.Quantity,
        LineTotal = dto.LineTotal,
    };
}
