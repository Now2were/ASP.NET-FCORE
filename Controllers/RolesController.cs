using ClothingStore.Api.Data;
using ClothingStore.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    private readonly EntityApiContext _context;

    public RolesController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/roles?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RoleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var roles = await _context.Roles
            .OrderBy(role => role.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return roles.Select(RoleMapper.ToDto).ToList();
    }

    // GET: api/roles/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoleDto>> GetById(int id)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(role => role.Id == id);

        if (role is null)
        {
            return NotFound();
        }

        return RoleMapper.ToDto(role);
    }

    // POST: api/roles
    [HttpPost]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RoleDto>> Create(CreateRoleDto dto)
    {
        var role = RoleMapper.ToEntity(dto);

        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = role.Id }, RoleMapper.ToDto(role));
    }

    // PUT: api/roles/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateRoleDto dto)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(role => role.Id == id);

        if (role is null)
        {
            return NotFound();
        }

        role.Name = dto.Name;
        role.Description = dto.Description;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/roles/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchRoleDto dto)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(role => role.Id == id);

        if (role is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.Name is not null)
        {
            role.Name = dto.Name;
            hasChanges = true;
        }

        if (dto.Description is not null)
        {
            role.Description = dto.Description;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/roles/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(role => role.Id == id);

        if (role is null)
        {
            return NotFound();
        }

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}