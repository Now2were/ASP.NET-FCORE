using ClothingStore.Api.Common;
using ClothingStore.Api.Models;
using AppException = ClothingStore.Api.Common.ApplicationException;

namespace ClothingStore.Api.Services;

/// <summary>Правила предметной области применяются к конечному состоянию после POST, PUT и PATCH.</summary>
public static class EntityRules
{
    public static void Validate(object entity)
    {
        var error = entity switch
        {
            ProductVariant v when v.StockQuantity < 0 || v.ReservedQuantity < 0 || v.StockQuantity < v.ReservedQuantity
                => "Остаток не может быть отрицательным или меньше зарезервированного количества.",
            ProductVariant v when v.PriceOverride is <= 0 => "Цена варианта должна быть положительной.",
            Product p when p.Gender > 3 => "Gender должен быть от 0 до 3.",
            Product p when p.Price <= 0 || p.SalePrice is <= 0 || p.SalePrice > p.Price => "Цена должна быть положительной; цена со скидкой не должна превышать обычную.",
            ProductMaterial m when m.Percent is <= 0 or > 100 => "Доля материала должна быть от 1 до 100.",
            Review r when r.Rating is < 1 or > 5 => "Оценка должна быть от 1 до 5.",
            CartItem c when c.Quantity <= 0 => "Количество должно быть положительным.",
            OrderItem i when i.Quantity <= 0 || i.UnitPrice < 0 || i.LineTotal < 0 => "Количество должно быть положительным, суммы неотрицательными.",
            Cart c when c.UserId is null && c.GuestToken is null => "У корзины должен быть UserId или GuestToken.",
            Payment p when p.Amount <= 0 => "Сумма оплаты должна быть положительной.",
            Order o when o.Subtotal < 0 || o.DiscountAmount < 0 || o.DeliveryCost < 0 || o.Total < 0 => "Суммы заказа не могут быть отрицательными.",
            User u when !new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(u.Email) => "Неверный email пользователя.",
            Category c when c.Id != 0 && c.ParentCategoryId == c.Id => "Категория не может быть своим родителем.",
            _ => null
        };
        if (error is not null) throw new AppException(ErrorKind.Validation, error);
    }
}
