using System;
using System.Collections.Generic;

namespace ClothingStore.Api.Models;

public partial class OrderStatus
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int SortOrder { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
