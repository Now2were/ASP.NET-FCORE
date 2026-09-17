using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>Role: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class RoleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

/// <summary>Данные для создания Role (Id и CreatedAt формируются на сервере).</summary>
public class CreateRoleDto
{
    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

/// <summary>Полное обновление Role: все поля обязательные.</summary>
public class UpdateRoleDto
{
    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

/// <summary>Частичное обновление Role: передаются только изменяемые поля (merge patch).</summary>
public class PatchRoleDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

/// <summary>Статический маппер Role -> DTO.</summary>
public static class RoleMapper
{
    public static RoleDto ToDto(Role role) => new()
    {
        Id = role.Id,
        Name = role.Name,
        Description = role.Description,
    };

    public static Role ToEntity(CreateRoleDto dto) => new()
    {
        Name = dto.Name,
        Description = dto.Description,
    };
}
