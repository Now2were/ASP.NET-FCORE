using System;
using System.Collections.Generic;

namespace ClothingStore.Api.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int MethodId { get; set; }

    public int StatusId { get; set; }

    public decimal Amount { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public string? ExternalTransactionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? PaidAt { get; set; }

    public virtual PaymentMethod Method { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual PaymentStatus Status { get; set; } = null!;
}
