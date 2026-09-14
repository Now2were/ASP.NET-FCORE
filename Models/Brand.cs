using System;
using System.Collections.Generic;

namespace ClothingStore.Api.Models;

public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Country { get; set; }

    public string? Description { get; set; }

    public string? LogoUrl { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
