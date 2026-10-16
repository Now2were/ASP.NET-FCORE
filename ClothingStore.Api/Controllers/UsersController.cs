using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/users")]
public class UsersController(IService<UserDto, CreateUserDto, UpdateUserDto, PatchUserDto> service)
    : CrudController<UserDto, CreateUserDto, UpdateUserDto, PatchUserDto>(service);
