using System;
using System.Collections.Generic;

namespace ClothingStore.Api.Models;

public partial class OrderItem
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

    public virtual Order Order { get; set; } = null!;

    public virtual ProductVariant? Variant { get; set; }
}
