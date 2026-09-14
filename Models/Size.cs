using System;
using System.Collections.Generic;

namespace ClothingStore.Api.Models;

public partial class Size
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int SortOrder { get; set; }

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
}
