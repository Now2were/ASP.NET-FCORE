using System.ComponentModel.DataAnnotations;
namespace ClothingStore.Api.DTO;

public class StockReservationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int VariantId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    public UserSummaryDto User { get; set; } = null!;
    public ProductVariantSummaryDto Variant { get; set; } = null!;
}
public class CreateStockReservationDto
{
    [Range(1, int.MaxValue)] public int UserId { get; set; }
    [Range(1, int.MaxValue)] public int VariantId { get; set; }
    [Range(1, int.MaxValue)] public int Quantity { get; set; }
}
public class UpdateStockReservationDto : CreateStockReservationDto;
public class PatchStockReservationDto : PatchDto
{
    private int? _userId, _variantId, _quantity;
    [Range(1, int.MaxValue)]
    public int? UserId { get => _userId; set => Set(ref _userId, value, nameof(UserId)); }
    [Range(1, int.MaxValue)]
    public int? VariantId { get => _variantId; set => Set(ref _variantId, value, nameof(VariantId)); }
    [Range(1, int.MaxValue)]
    public int? Quantity { get => _quantity; set => Set(ref _quantity, value, nameof(Quantity)); }
}
