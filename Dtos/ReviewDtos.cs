using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.Dtos;

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
public class PatchReviewDto
{
    public int? ProductId { get; set; }
    public int? UserId { get; set; }
    [Range(1, 5)]
    public byte? Rating { get; set; }
    public string? Title { get; set; }
    public string? Comment { get; set; }
    public bool? IsRecommended { get; set; }
    public bool? IsModerated { get; set; }
}

/// <summary>Статический маппер Review -> DTO.</summary>
public static class ReviewMapper
{
    public static ReviewDto ToDto(Review review) => new()
    {
        Id = review.Id,
        ProductId = review.ProductId,
        UserId = review.UserId,
        Rating = review.Rating,
        Title = review.Title,
        Comment = review.Comment,
        IsRecommended = review.IsRecommended,
        IsModerated = review.IsModerated,
        CreatedAt = review.CreatedAt,
    };

    public static Review ToEntity(CreateReviewDto dto) => new()
    {
        ProductId = dto.ProductId,
        UserId = dto.UserId,
        Rating = dto.Rating,
        Title = dto.Title,
        Comment = dto.Comment,
        IsRecommended = dto.IsRecommended,
        IsModerated = dto.IsModerated,
    };
}
