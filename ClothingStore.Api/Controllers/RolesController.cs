using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/roles")]
public class RolesController(IService<RoleDto, CreateRoleDto, UpdateRoleDto, PatchRoleDto> service)
    : CrudController<RoleDto, CreateRoleDto, UpdateRoleDto, PatchRoleDto>(service);
