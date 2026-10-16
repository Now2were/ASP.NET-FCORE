using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/carts")]
public class CartsController(IService<CartDto, CreateCartDto, UpdateCartDto, PatchCartDto> service)
    : CrudController<CartDto, CreateCartDto, UpdateCartDto, PatchCartDto>(service);
