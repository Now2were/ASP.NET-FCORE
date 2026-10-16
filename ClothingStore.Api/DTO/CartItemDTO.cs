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
    public CartSummaryDto? Cart { get; set; }
    public ProductVariantSummaryDto? Variant { get; set; }
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
public class PatchCartItemDto : PatchDto
{
    private int? _cartId;
    public int? CartId { get => _cartId; set => Set(ref _cartId, value, nameof(CartId)); }
    private int? _variantId;
    public int? VariantId { get => _variantId; set => Set(ref _variantId, value, nameof(VariantId)); }
    private int? _quantity;
    [Range(1, 2147483647)]
    public int? Quantity { get => _quantity; set => Set(ref _quantity, value, nameof(Quantity)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class CartItemSummaryDto
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int VariantId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
}
