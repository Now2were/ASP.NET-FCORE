using ClothingStore.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Api.DTO;

/// <summary>Role: объект, возвращаемый клиенту (все поля, включая Id и CreatedAt).</summary>
public class RoleDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public List<UserSummaryDto> Users { get; set; } = new();
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
public class PatchRoleDto : PatchDto
{
    private string? _name;
    public string? Name { get => _name; set => Set(ref _name, value, nameof(Name)); }
    private string? _description;
    public string? Description { get => _description; set => Set(ref _description, value, nameof(Description)); }
}

/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class RoleSummaryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
