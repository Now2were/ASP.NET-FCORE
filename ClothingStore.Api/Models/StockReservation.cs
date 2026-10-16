namespace ClothingStore.Api.Models;
/// <summary>Факт резервирования: кто, какой вариант, сколько, когда; отмена сохраняет историю.</summary>
public class StockReservation
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int VariantId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public User User { get; set; } = null!;
    public ProductVariant Variant { get; set; } = null!;
}
