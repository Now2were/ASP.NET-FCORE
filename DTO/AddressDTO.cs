using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>Address: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class AddressDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string? Region { get; set; }
    public string City { get; set; } = null!;
    public string? Street { get; set; }
    public string? House { get; set; }
    public string? Apartment { get; set; }
    public string? PostIndex { get; set; }
    public bool IsDefault { get; set; }
}

/// <summary>Данные для создания Address (Id и CreatedAt формируются на сервере).</summary>
public class CreateAddressDto
{
    public int UserId { get; set; }
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    public string Phone { get; set; } = null!;
    [Required]
    public string Country { get; set; } = null!;
    public string? Region { get; set; }
    [Required]
    public string City { get; set; } = null!;
    public string? Street { get; set; }
    public string? House { get; set; }
    public string? Apartment { get; set; }
    public string? PostIndex { get; set; }
    public bool IsDefault { get; set; }
}

/// <summary>Полное обновление Address: все поля обязательные.</summary>
public class UpdateAddressDto
{
    public int UserId { get; set; }
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    public string Phone { get; set; } = null!;
    [Required]
    public string Country { get; set; } = null!;
    public string? Region { get; set; }
    [Required]
    public string City { get; set; } = null!;
    public string? Street { get; set; }
    public string? House { get; set; }
    public string? Apartment { get; set; }
    public string? PostIndex { get; set; }
    public bool IsDefault { get; set; }
}

/// <summary>Частичное обновление Address: передаются только изменяемые поля (merge patch).</summary>
public class PatchAddressDto
{
    public int? UserId { get; set; }
    public string? FullName { get; set; }
    public string? Phone { get; set; }
    public string? Country { get; set; }
    public string? Region { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? House { get; set; }
    public string? Apartment { get; set; }
    public string? PostIndex { get; set; }
    public bool? IsDefault { get; set; }
}

/// <summary>Статический маппер Address -> DTO.</summary>
public static class AddressMapper
{
    public static AddressDto ToDto(Address address) => new()
    {
        Id = address.Id,
        UserId = address.UserId,
        FullName = address.FullName,
        Phone = address.Phone,
        Country = address.Country,
        Region = address.Region,
        City = address.City,
        Street = address.Street,
        House = address.House,
        Apartment = address.Apartment,
        PostIndex = address.PostIndex,
        IsDefault = address.IsDefault,
    };

    public static Address ToEntity(CreateAddressDto dto) => new()
    {
        UserId = dto.UserId,
        FullName = dto.FullName,
        Phone = dto.Phone,
        Country = dto.Country,
        Region = dto.Region,
        City = dto.City,
        Street = dto.Street,
        House = dto.House,
        Apartment = dto.Apartment,
        PostIndex = dto.PostIndex,
        IsDefault = dto.IsDefault,
    };
}
