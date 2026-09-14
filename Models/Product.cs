using System;
using System.Collections.Generic;

namespace ClothingStore.Api.Models;

public partial class Product
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public byte Gender { get; set; }

    public decimal Price { get; set; }

    public decimal? SalePrice { get; set; }

    public bool IsActive { get; set; }

    public bool IsBestseller { get; set; }

    public decimal AverageRating { get; set; }

    public int RatingCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ProductImage? ProductImage { get; set; }

    public virtual ICollection<ProductMaterial> ProductMaterials { get; set; } = new List<ProductMaterial>();

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
