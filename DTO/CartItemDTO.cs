using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>CartItem: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class CartItemDto
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int VariantId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
}

/// <summary>Данные для создания CartItem (Id и CreatedAt формируются на сервере).</summary>
public class CreateCartItemDto
{
    public int CartId { get; set; }
    public int VariantId { get; set; }
    [Range(1, 2147483647)]
    public int Quantity { get; set; }
}

/// <summary>Полное обновление CartItem: все поля обязательные.</summary>
public class UpdateCartItemDto
{
    public int CartId { get; set; }
    public int VariantId { get; set; }
    [Range(1, 2147483647)]
    public int Quantity { get; set; }
}

/// <summary>Частичное обновление CartItem: передаются только изменяемые поля (merge patch).</summary>
public class PatchCartItemDto
{
    public int? CartId { get; set; }
    public int? VariantId { get; set; }
    [Range(1, 2147483647)]
    public int? Quantity { get; set; }
}

/// <summary>Статический маппер CartItem -> DTO.</summary>
public static class CartItemMapper
{
    public static CartItemDto ToDto(CartItem cartItem) => new()
    {
        Id = cartItem.Id,
        CartId = cartItem.CartId,
        VariantId = cartItem.VariantId,
        Quantity = cartItem.Quantity,
        CreatedAt = cartItem.CreatedAt,
    };

    public static CartItem ToEntity(CreateCartItemDto dto) => new()
    {
        CartId = dto.CartId,
        VariantId = dto.VariantId,
        Quantity = dto.Quantity,
    };
}
