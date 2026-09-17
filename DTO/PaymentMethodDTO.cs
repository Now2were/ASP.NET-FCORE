using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>PaymentMethod: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class PaymentMethodDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Данные для создания PaymentMethod (Id и CreatedAt формируются на сервере).</summary>
public class CreatePaymentMethodDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Полное обновление PaymentMethod: все поля обязательные.</summary>
public class UpdatePaymentMethodDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}

/// <summary>Частичное обновление PaymentMethod: передаются только изменяемые поля (merge patch).</summary>
public class PatchPaymentMethodDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public int? SortOrder { get; set; }
    public bool? IsActive { get; set; }
}

/// <summary>Статический маппер PaymentMethod -> DTO.</summary>
public static class PaymentMethodMapper
{
    public static PaymentMethodDto ToDto(PaymentMethod paymentMethod) => new()
    {
        Id = paymentMethod.Id,
        Code = paymentMethod.Code,
        Name = paymentMethod.Name,
        SortOrder = paymentMethod.SortOrder,
        IsActive = paymentMethod.IsActive,
    };

    public static PaymentMethod ToEntity(CreatePaymentMethodDto dto) => new()
    {
        Code = dto.Code,
        Name = dto.Name,
        SortOrder = dto.SortOrder,
        IsActive = dto.IsActive,
    };
}
