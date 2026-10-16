using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>Cart: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class CartDto
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public Guid? GuestToken { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<CartItemSummaryDto> CartItems { get; set; } = new();
    public UserSummaryDto? User { get; set; }
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
public class PatchCartDto : PatchDto
{
    private int? _userId;
    public int? UserId { get => _userId; set => Set(ref _userId, value, nameof(UserId)); }
    private Guid? _guestToken;
    public Guid? GuestToken { get => _guestToken; set => Set(ref _guestToken, value, nameof(GuestToken)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class CartSummaryDto
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public Guid? GuestToken { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
