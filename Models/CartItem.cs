using System;
using System.Collections.Generic;

namespace ClothingStore.Api.Models;

public partial class CartItem
{
    public int Id { get; set; }

    public int CartId { get; set; }

    public int VariantId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual ProductVariant Variant { get; set; } = null!;
}
