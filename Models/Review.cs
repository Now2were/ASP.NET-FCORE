using System;
using System.Collections.Generic;

namespace ClothingStore.Api.Models;

public partial class Review
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int UserId { get; set; }

    public byte Rating { get; set; }

    public string? Title { get; set; }

    public string? Comment { get; set; }

    public bool? IsRecommended { get; set; }

    public bool IsModerated { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
