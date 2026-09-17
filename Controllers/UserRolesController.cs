using ClothingStore.Api.Data;
using ClothingStore.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

/// <summary>
/// UserRoles — служебная таблица связи "пользователь-роль" с составным ключом (UserId, RoleId)
/// и без неключевых полей, поэтому PUT и PATCH для неё не имеют смысла.
/// </summary>
[ApiController]
[Route("api/user-roles")]
public class UserRolesController : ControllerBase
{
    private readonly EntityApiContext _context;

    public UserRolesController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/user-roles?page=1&pageSize=10
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var userRoles = await _context.UserRoles
            .OrderBy(ur => ur.UserId)
            .ThenBy(ur => ur.RoleId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return userRoles.Select(UserRoleMapper.ToDto).ToList();
    }

    // GET: api/user-roles/3/1
    [HttpGet("{userId:int}/{roleId:int}")]
    public async Task<ActionResult<UserRoleDto>> GetById(int userId, int roleId)
    {
        var userRole = await _context.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

        if (userRole is null)
        {
            return NotFound();
        }

        return UserRoleMapper.ToDto(userRole);
    }

    // POST: api/user-roles
    [HttpPost]
    public async Task<ActionResult<UserRoleDto>> Create(CreateUserRoleDto dto)
    {
        var exists = await _context.UserRoles
            .AnyAsync(ur => ur.UserId == dto.UserId && ur.RoleId == dto.RoleId);

        if (exists)
        {
            return Conflict(new { message = $"Роль {dto.RoleId} уже назначена пользователю {dto.UserId}." });
        }

        var userRole = UserRoleMapper.ToEntity(dto);

        _context.UserRoles.Add(userRole);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { userId = userRole.UserId, roleId = userRole.RoleId }, UserRoleMapper.ToDto(userRole));
    }

    // DELETE: api/user-roles/3/1
    [HttpDelete("{userId:int}/{roleId:int}")]
    public async Task<IActionResult> Delete(int userId, int roleId)
    {
        var userRole = await _context.UserRoles
            .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

        if (userRole is null)
        {
            return NotFound();
        }

        _context.UserRoles.Remove(userRole);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}