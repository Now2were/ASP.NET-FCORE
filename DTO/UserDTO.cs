using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>User: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
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
public class PatchUserDto
{
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? LastLoginAt { get; set; }
}

/// <summary>Статический маппер User -> DTO.</summary>
public static class UserMapper
{
    public static UserDto ToDto(User user) => new()
    {
        Id = user.Id,
        Email = user.Email,
        PasswordHash = user.PasswordHash,
        FirstName = user.FirstName,
        LastName = user.LastName,
        PhoneNumber = user.PhoneNumber,
        IsActive = user.IsActive,
        CreatedAt = user.CreatedAt,
        LastLoginAt = user.LastLoginAt,
    };

    public static User ToEntity(CreateUserDto dto) => new()
    {
        Email = dto.Email,
        PasswordHash = dto.PasswordHash,
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        PhoneNumber = dto.PhoneNumber,
        IsActive = dto.IsActive,
        LastLoginAt = dto.LastLoginAt,
    };
}
