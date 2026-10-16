using System.Globalization;
using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;

[Route("api/user-roles")]
public class UserRolesController(IService<UserRoleDto, CreateUserRoleDto, UpdateUserRoleDto, PatchUserRoleDto> service) : ApiControllerBase
{
    [HttpGet]
    public Task<IActionResult> GetAll(int page = 1, int pageSize = 10, CancellationToken ct = default) => ExecuteAsync(async () =>
    {
        var result = await service.GetPageAsync(page, pageSize, ct);
        Response.Headers["X-Total-Count"] = result.TotalCount.ToString(CultureInfo.InvariantCulture);
        return Ok(result.Items);
    });
    [HttpGet("{userId:int}/{roleId:int}")]
    public Task<IActionResult> GetById(int userId, int roleId, CancellationToken ct) => ExecuteAsync(async () =>
        Ok(await service.GetAsync([userId, roleId], ct)));
    [HttpPost]
    public Task<IActionResult> Create(CreateUserRoleDto dto, CancellationToken ct) => ExecuteAsync(async () =>
    {
        var result = await service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { result.UserId, result.RoleId }, result);
    });
    [HttpPut("{userId:int}/{roleId:int}")]
    public Task<IActionResult> Update(int userId, int roleId, UpdateUserRoleDto dto, CancellationToken ct) => ExecuteAsync(async () =>
    { await service.UpdateAsync([userId, roleId], dto, ct); return NoContent(); });
    [HttpPatch("{userId:int}/{roleId:int}")]
    public Task<IActionResult> Patch(int userId, int roleId, PatchUserRoleDto dto, CancellationToken ct) => ExecuteAsync(async () =>
    { await service.PatchAsync([userId, roleId], dto, ct); return NoContent(); });
    [HttpDelete("{userId:int}/{roleId:int}")]
    public Task<IActionResult> Delete(int userId, int roleId, CancellationToken ct) => ExecuteAsync(async () =>
    { await service.DeleteAsync([userId, roleId], ct); return NoContent(); });
}
