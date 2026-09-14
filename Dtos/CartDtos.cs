using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.Dtos;

/// <summary>Cart: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class CartDto
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public Guid? GuestToken { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>Данные для создания Cart (Id и CreatedAt формируются на сервере).</summary>
public class CreateCartDto
{
    public int? UserId { get; set; }
    public Guid? GuestToken { get; set; }
}

/// <summary>Полное обновление Cart: все поля обязательные.</summary>
public class UpdateCartDto
{
    public int? UserId { get; set; }
    public Guid? GuestToken { get; set; }
}

/// <summary>Частичное обновление Cart: передаются только изменяемые поля (merge patch).</summary>
public class PatchCartDto
{
    public int? UserId { get; set; }
    public Guid? GuestToken { get; set; }
}

/// <summary>Статический маппер Cart -> DTO.</summary>
public static class CartMapper
{
    public static CartDto ToDto(Cart cart) => new()
    {
        Id = cart.Id,
        UserId = cart.UserId,
        GuestToken = cart.GuestToken,
        CreatedAt = cart.CreatedAt,
        UpdatedAt = cart.UpdatedAt,
    };

    public static Cart ToEntity(CreateCartDto dto) => new()
    {
        UserId = dto.UserId,
        GuestToken = dto.GuestToken,
    };
}
