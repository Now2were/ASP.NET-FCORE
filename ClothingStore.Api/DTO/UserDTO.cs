using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>User: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public List<AddressSummaryDto> Addresses { get; set; } = new();
    public CartSummaryDto? Cart { get; set; }
    public List<OrderSummaryDto> Orders { get; set; } = new();
    public List<ReviewSummaryDto> Reviews { get; set; } = new();
    public List<RoleSummaryDto> Roles { get; set; } = new();
}

/// <summary>Данные для создания User (Id и CreatedAt формируются на сервере).</summary>
public class CreateUserDto
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string PasswordHash { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastLoginAt { get; set; }
}

/// <summary>Полное обновление User: все поля обязательные.</summary>
public class UpdateUserDto
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string PasswordHash { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastLoginAt { get; set; }
}

/// <summary>Частичное обновление User: передаются только изменяемые поля (merge patch).</summary>
public class PatchUserDto : PatchDto
{
    private string? _email;
    public string? Email { get => _email; set => Set(ref _email, value, nameof(Email)); }
    private string? _passwordHash;
    public string? PasswordHash { get => _passwordHash; set => Set(ref _passwordHash, value, nameof(PasswordHash)); }
    private string? _firstName;
    public string? FirstName { get => _firstName; set => Set(ref _firstName, value, nameof(FirstName)); }
    private string? _lastName;
    public string? LastName { get => _lastName; set => Set(ref _lastName, value, nameof(LastName)); }
    private string? _phoneNumber;
    public string? PhoneNumber { get => _phoneNumber; set => Set(ref _phoneNumber, value, nameof(PhoneNumber)); }
    private bool? _isActive;
    public bool? IsActive { get => _isActive; set => Set(ref _isActive, value, nameof(IsActive)); }
    private DateTime? _lastLoginAt;
    public DateTime? LastLoginAt { get => _lastLoginAt; set => Set(ref _lastLoginAt, value, nameof(LastLoginAt)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class UserSummaryDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
}
