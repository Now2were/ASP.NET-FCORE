using System;
using System.Collections.Generic;

namespace ClothingStore.Api.Models;

public partial class Order
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

    public virtual DeliveryMethod DeliveryMethod { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual OrderStatus Status { get; set; } = null!;

    public virtual User? User { get; set; }
}
