using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.Dtos;

/// <summary>PaymentStatus: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class PaymentStatusDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
}

/// <summary>Данные для создания PaymentStatus (Id и CreatedAt формируются на сервере).</summary>
public class CreatePaymentStatusDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
}

/// <summary>Полное обновление PaymentStatus: все поля обязательные.</summary>
public class UpdatePaymentStatusDto
{
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string Name { get; set; } = null!;
}

/// <summary>Частичное обновление PaymentStatus: передаются только изменяемые поля (merge patch).</summary>
public class PatchPaymentStatusDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}

/// <summary>Статический маппер PaymentStatus -> DTO.</summary>
public static class PaymentStatusMapper
{
    public static PaymentStatusDto ToDto(PaymentStatus paymentStatus) => new()
    {
        Id = paymentStatus.Id,
        Code = paymentStatus.Code,
        Name = paymentStatus.Name,
    };

    public static PaymentStatus ToEntity(CreatePaymentStatusDto dto) => new()
    {
        Code = dto.Code,
        Name = dto.Name,
    };
}
