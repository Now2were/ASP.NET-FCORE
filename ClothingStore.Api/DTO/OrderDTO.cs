using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

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
    public DeliveryMethodSummaryDto? DeliveryMethod { get; set; }
    public List<OrderItemSummaryDto> OrderItems { get; set; } = new();
    public List<PaymentSummaryDto> Payments { get; set; } = new();
    public OrderStatusSummaryDto? Status { get; set; }
    public UserSummaryDto? User { get; set; }
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
public class PatchOrderDto : PatchDto
{
    private string? _orderNumber;
    public string? OrderNumber { get => _orderNumber; set => Set(ref _orderNumber, value, nameof(OrderNumber)); }
    private int? _userId;
    public int? UserId { get => _userId; set => Set(ref _userId, value, nameof(UserId)); }
    private int? _statusId;
    public int? StatusId { get => _statusId; set => Set(ref _statusId, value, nameof(StatusId)); }
    private decimal? _subtotal;
    [Range(0, 99999999.99)]
    public decimal? Subtotal { get => _subtotal; set => Set(ref _subtotal, value, nameof(Subtotal)); }
    private decimal? _discountAmount;
    [Range(0, 99999999.99)]
    public decimal? DiscountAmount { get => _discountAmount; set => Set(ref _discountAmount, value, nameof(DiscountAmount)); }
    private decimal? _deliveryCost;
    [Range(0, 99999999.99)]
    public decimal? DeliveryCost { get => _deliveryCost; set => Set(ref _deliveryCost, value, nameof(DeliveryCost)); }
    private decimal? _total;
    [Range(0, 99999999.99)]
    public decimal? Total { get => _total; set => Set(ref _total, value, nameof(Total)); }
    private string? _currencyCode;
    public string? CurrencyCode { get => _currencyCode; set => Set(ref _currencyCode, value, nameof(CurrencyCode)); }
    private int? _deliveryMethodId;
    public int? DeliveryMethodId { get => _deliveryMethodId; set => Set(ref _deliveryMethodId, value, nameof(DeliveryMethodId)); }
    private string? _recipientName;
    public string? RecipientName { get => _recipientName; set => Set(ref _recipientName, value, nameof(RecipientName)); }
    private string? _recipientPhone;
    public string? RecipientPhone { get => _recipientPhone; set => Set(ref _recipientPhone, value, nameof(RecipientPhone)); }
    private string? _recipientEmail;
    public string? RecipientEmail { get => _recipientEmail; set => Set(ref _recipientEmail, value, nameof(RecipientEmail)); }
    private string? _country;
    public string? Country { get => _country; set => Set(ref _country, value, nameof(Country)); }
    private string? _region;
    public string? Region { get => _region; set => Set(ref _region, value, nameof(Region)); }
    private string? _city;
    public string? City { get => _city; set => Set(ref _city, value, nameof(City)); }
    private string? _street;
    public string? Street { get => _street; set => Set(ref _street, value, nameof(Street)); }
    private string? _house;
    public string? House { get => _house; set => Set(ref _house, value, nameof(House)); }
    private string? _apartment;
    public string? Apartment { get => _apartment; set => Set(ref _apartment, value, nameof(Apartment)); }
    private string? _postIndex;
    public string? PostIndex { get => _postIndex; set => Set(ref _postIndex, value, nameof(PostIndex)); }
    private string? _trackingNumber;
    public string? TrackingNumber { get => _trackingNumber; set => Set(ref _trackingNumber, value, nameof(TrackingNumber)); }
    private string? _comment;
    public string? Comment { get => _comment; set => Set(ref _comment, value, nameof(Comment)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class OrderSummaryDto
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
