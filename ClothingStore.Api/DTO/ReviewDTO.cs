using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>Review: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class ReviewDto
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
    public ProductSummaryDto? Product { get; set; }
    public UserSummaryDto? User { get; set; }
}

/// <summary>Данные для создания Review (Id и CreatedAt формируются на сервере).</summary>
public class CreateReviewDto
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
    [Range(1, 5)]
    public byte Rating { get; set; }
    public string? Title { get; set; }
    public string? Comment { get; set; }
    public bool? IsRecommended { get; set; }
    public bool IsModerated { get; set; }
}

/// <summary>Полное обновление Review: все поля обязательные.</summary>
public class UpdateReviewDto
{
    public int ProductId { get; set; }
    public int UserId { get; set; }
    [Range(1, 5)]
    public byte Rating { get; set; }
    public string? Title { get; set; }
    public string? Comment { get; set; }
    public bool? IsRecommended { get; set; }
    public bool IsModerated { get; set; }
}

/// <summary>Частичное обновление Review: передаются только изменяемые поля (merge patch).</summary>
public class PatchReviewDto : PatchDto
{
    private int? _productId;
    public int? ProductId { get => _productId; set => Set(ref _productId, value, nameof(ProductId)); }
    private int? _userId;
    public int? UserId { get => _userId; set => Set(ref _userId, value, nameof(UserId)); }
    private byte? _rating;
    [Range(1, 5)]
    public byte? Rating { get => _rating; set => Set(ref _rating, value, nameof(Rating)); }
    private string? _title;
    public string? Title { get => _title; set => Set(ref _title, value, nameof(Title)); }
    private string? _comment;
    public string? Comment { get => _comment; set => Set(ref _comment, value, nameof(Comment)); }
    private bool? _isRecommended;
    public bool? IsRecommended { get => _isRecommended; set => Set(ref _isRecommended, value, nameof(IsRecommended)); }
    private bool? _isModerated;
    public bool? IsModerated { get => _isModerated; set => Set(ref _isModerated, value, nameof(IsModerated)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class ReviewSummaryDto
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
}
