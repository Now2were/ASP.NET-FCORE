using ClothingStore.Api.Data;
using ClothingStore.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly EntityApiContext _context;

    public UsersController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/users?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var users = await _context.Users
            .OrderBy(user => user.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return users.Select(UserMapper.ToDto).ToList();
    }

    // GET: api/users/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.Id == id);

        if (user is null)
        {
            return NotFound();
        }

        return UserMapper.ToDto(user);
    }

    // POST: api/users
    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> Create(CreateUserDto dto)
    {
        var user = UserMapper.ToEntity(dto);
        user.CreatedAt = DateTime.UtcNow;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, UserMapper.ToDto(user));
    }

    // PUT: api/users/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateUserDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.Id == id);

        if (user is null)
        {
            return NotFound();
        }

        user.Email = dto.Email;
        user.PasswordHash = dto.PasswordHash;
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.PhoneNumber = dto.PhoneNumber;
        user.IsActive = dto.IsActive;
        user.LastLoginAt = dto.LastLoginAt;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/users/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchUserDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.Id == id);

        if (user is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.Email is not null)
        {
            user.Email = dto.Email;
            hasChanges = true;
        }

        if (dto.PasswordHash is not null)
        {
            user.PasswordHash = dto.PasswordHash;
            hasChanges = true;
        }

        if (dto.FirstName is not null)
        {
            user.FirstName = dto.FirstName;
            hasChanges = true;
        }

        if (dto.LastName is not null)
        {
            user.LastName = dto.LastName;
            hasChanges = true;
        }

        if (dto.PhoneNumber is not null)
        {
            user.PhoneNumber = dto.PhoneNumber;
            hasChanges = true;
        }

        if (dto.IsActive is not null)
        {
            user.IsActive = dto.IsActive.Value;
            hasChanges = true;
        }

        if (dto.LastLoginAt is not null)
        {
            user.LastLoginAt = dto.LastLoginAt.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/users/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(user => user.Id == id);

        if (user is null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}