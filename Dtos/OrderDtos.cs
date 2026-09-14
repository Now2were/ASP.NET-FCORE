using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.Dtos;

/// <summary>Order: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class OrderDto
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = null!;
    public int? UserId { get; set; }
    public int StatusId { get; set; }
    public decimal Subtotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal DeliveryCost { get; set; }
    public decimal Total { get; set; }
    public string CurrencyCode { get; set; } = null!;
    public int DeliveryMethodId { get; set; }
    public string RecipientName { get; set; } = null!;
    public string RecipientPhone { get; set; } = null!;
    public string? RecipientEmail { get; set; }
    public string Country { get; set; } = null!;
    public string? Region { get; set; }
    public string City { get; set; } = null!;
    public string? Street { get; set; }
    public string? House { get; set; }
    public string? Apartment { get; set; }
    public string? PostIndex { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>Данные для создания Order (Id и CreatedAt формируются на сервере).</summary>
public class CreateOrderDto
{
    [Required]
    public string OrderNumber { get; set; } = null!;
    public int? UserId { get; set; }
    public int StatusId { get; set; }
    [Range(0, 99999999.99)]
    public decimal Subtotal { get; set; }
    [Range(0, 99999999.99)]
    public decimal DiscountAmount { get; set; }
    [Range(0, 99999999.99)]
    public decimal DeliveryCost { get; set; }
    [Range(0, 99999999.99)]
    public decimal Total { get; set; }
    [Required]
    public string CurrencyCode { get; set; } = null!;
    public int DeliveryMethodId { get; set; }
    [Required]
    public string RecipientName { get; set; } = null!;
    [Required]
    public string RecipientPhone { get; set; } = null!;
    public string? RecipientEmail { get; set; }
    [Required]
    public string Country { get; set; } = null!;
    public string? Region { get; set; }
    [Required]
    public string City { get; set; } = null!;
    public string? Street { get; set; }
    public string? House { get; set; }
    public string? Apartment { get; set; }
    public string? PostIndex { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Comment { get; set; }
}

/// <summary>Полное обновление Order: все поля обязательные.</summary>
public class UpdateOrderDto
{
    [Required]
    public string OrderNumber { get; set; } = null!;
    public int? UserId { get; set; }
    public int StatusId { get; set; }
    [Range(0, 99999999.99)]
    public decimal Subtotal { get; set; }
    [Range(0, 99999999.99)]
    public decimal DiscountAmount { get; set; }
    [Range(0, 99999999.99)]
    public decimal DeliveryCost { get; set; }
    [Range(0, 99999999.99)]
    public decimal Total { get; set; }
    [Required]
    public string CurrencyCode { get; set; } = null!;
    public int DeliveryMethodId { get; set; }
    [Required]
    public string RecipientName { get; set; } = null!;
    [Required]
    public string RecipientPhone { get; set; } = null!;
    public string? RecipientEmail { get; set; }
    [Required]
    public string Country { get; set; } = null!;
    public string? Region { get; set; }
    [Required]
    public string City { get; set; } = null!;
    public string? Street { get; set; }
    public string? House { get; set; }
    public string? Apartment { get; set; }
    public string? PostIndex { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Comment { get; set; }
}

/// <summary>Частичное обновление Order: передаются только изменяемые поля (merge patch).</summary>
public class PatchOrderDto
{
    public string? OrderNumber { get; set; }
    public int? UserId { get; set; }
    public int? StatusId { get; set; }
    [Range(0, 99999999.99)]
    public decimal? Subtotal { get; set; }
    [Range(0, 99999999.99)]
    public decimal? DiscountAmount { get; set; }
    [Range(0, 99999999.99)]
    public decimal? DeliveryCost { get; set; }
    [Range(0, 99999999.99)]
    public decimal? Total { get; set; }
    public string? CurrencyCode { get; set; }
    public int? DeliveryMethodId { get; set; }
    public string? RecipientName { get; set; }
    public string? RecipientPhone { get; set; }
    public string? RecipientEmail { get; set; }
    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? House { get; set; }
    public string? Apartment { get; set; }
    public string? PostIndex { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Comment { get; set; }
}

/// <summary>Статический маппер Order -> DTO.</summary>
public static class OrderMapper
{
    public static OrderDto ToDto(Order order) => new()
    {
        Id = order.Id,
        OrderNumber = order.OrderNumber,
        UserId = order.UserId,
        StatusId = order.StatusId,
        Subtotal = order.Subtotal,
        DiscountAmount = order.DiscountAmount,
        DeliveryCost = order.DeliveryCost,
        Total = order.Total,
        CurrencyCode = order.CurrencyCode,
        DeliveryMethodId = order.DeliveryMethodId,
        RecipientName = order.RecipientName,
        RecipientPhone = order.RecipientPhone,
        RecipientEmail = order.RecipientEmail,
        Country = order.Country,
        Region = order.Region,
        City = order.City,
        Street = order.Street,
        House = order.House,
        Apartment = order.Apartment,
        PostIndex = order.PostIndex,
        TrackingNumber = order.TrackingNumber,
        Comment = order.Comment,
        CreatedAt = order.CreatedAt,
        UpdatedAt = order.UpdatedAt,
    };

    public static Order ToEntity(CreateOrderDto dto) => new()
    {
        OrderNumber = dto.OrderNumber,
        UserId = dto.UserId,
        StatusId = dto.StatusId,
        Subtotal = dto.Subtotal,
        DiscountAmount = dto.DiscountAmount,
        DeliveryCost = dto.DeliveryCost,
        Total = dto.Total,
        CurrencyCode = dto.CurrencyCode,
        DeliveryMethodId = dto.DeliveryMethodId,
        RecipientName = dto.RecipientName,
        RecipientPhone = dto.RecipientPhone,
        RecipientEmail = dto.RecipientEmail,
        Country = dto.Country,
        Region = dto.Region,
        City = dto.City,
        Street = dto.Street,
        House = dto.House,
        Apartment = dto.Apartment,
        PostIndex = dto.PostIndex,
        TrackingNumber = dto.TrackingNumber,
        Comment = dto.Comment,
    };
}
