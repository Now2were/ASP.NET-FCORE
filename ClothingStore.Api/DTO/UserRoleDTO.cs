using ClothingStore.Api.Models;

namespace ClothingStore.Api.DTO;

public class UserRoleDto
{
    public int UserId { get; set; }

    public int RoleId { get; set; }
    public UserSummaryDto? User { get; set; }
    public RoleSummaryDto? Role { get; set; }
}

public class CreateUserRoleDto
{
    public int UserId { get; set; }

    public int RoleId { get; set; }
}

public class UpdateUserRoleDto
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}

public class PatchUserRoleDto : PatchDto
{
    private int? _userId;
    public int? UserId { get => _userId; set => Set(ref _userId, value, nameof(UserId)); }
    private int? _roleId;
    public int? RoleId { get => _roleId; set => Set(ref _roleId, value, nameof(RoleId)); }
}


/// <summary>Вложенные данные без обратных навигаций и циклов.</summary>
public class UserRoleSummaryDto
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}
