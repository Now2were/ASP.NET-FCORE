using System;
using System.Collections.Generic;

namespace ClothingStore.Api.Models;

public partial class Address
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string? Region { get; set; }

    public string City { get; set; } = null!;

    public string? Street { get; set; }

    public string? House { get; set; }

    public string? Apartment { get; set; }

    public string? PostIndex { get; set; }

    public bool IsDefault { get; set; }

    public virtual User User { get; set; } = null!;
}
