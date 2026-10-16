using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/cart-items")]
public class CartItemsController(IService<CartItemDto, CreateCartItemDto, UpdateCartItemDto, PatchCartItemDto> service)
    : CrudController<CartItemDto, CreateCartItemDto, UpdateCartItemDto, PatchCartItemDto>(service);
