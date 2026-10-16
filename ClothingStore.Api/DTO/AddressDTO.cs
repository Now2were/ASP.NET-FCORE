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
    public UserSummaryDto? User { get; set; }
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
public class PatchAddressDto : PatchDto
{
    private int? _userId;
    public int? UserId { get => _userId; set => Set(ref _userId, value, nameof(UserId)); }
    private string? _fullName;
    public string? FullName { get => _fullName; set => Set(ref _fullName, value, nameof(FullName)); }
    private string? _phone;
    public string? Phone { get => _phone; set => Set(ref _phone, value, nameof(Phone)); }
    private string? _country;
    public string? Country { get => _country; set => Set(ref _country, value, nameof(Country)); }
    private string? _region;
    public string? Region { get => _region; set => Set(ref _region, value, nameof(Region)); }
    private string? _city;
    public string? City { get => _city; set => Set(ref _city, value, nameof(City)); }
    private string? _street;
    public string? Street { get => _street; set => Set(ref _street, value, nameof(Street)); }
    private string? _house;
    public string? House { get => _house; set => Set(ref _house, value, nameof(House)); }
    private string? _apartment;
    public string? Apartment { get => _apartment; set => Set(ref _apartment, value, nameof(Apartment)); }
    private string? _postIndex;
    public string? PostIndex { get => _postIndex; set => Set(ref _postIndex, value, nameof(PostIndex)); }
    private bool? _isDefault;
    public bool? IsDefault { get => _isDefault; set => Set(ref _isDefault, value, nameof(IsDefault)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class AddressSummaryDto
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
