using System;
using System.Collections.Generic;

namespace ClothingStore.Api.Models;

public partial class ProductMaterial
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string MaterialName { get; set; } = null!;

    public byte? Percent { get; set; }

    public int SortOrder { get; set; }

    public virtual Product Product { get; set; } = null!;
}
