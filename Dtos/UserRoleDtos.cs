using ClothingStore.Api.Models;

namespace ClothingStore.Api.Dtos;

public class UserRoleDto
{
    public int UserId { get; set; }

    public int RoleId { get; set; }
}

public class CreateUserRoleDto
{
    public int UserId { get; set; }

    public int RoleId { get; set; }
}

public static class UserRoleMapper
{
    public static UserRoleDto ToDto(UserRole userRole) => new()
    {
        UserId = userRole.UserId,
        RoleId = userRole.RoleId,
    };

    public static UserRole ToEntity(CreateUserRoleDto dto) => new()
    {
        UserId = dto.UserId,
        RoleId = dto.RoleId,
    };
}
