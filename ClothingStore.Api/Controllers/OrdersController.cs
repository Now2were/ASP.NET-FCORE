using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/orders")]
public class OrdersController(IService<OrderDto, CreateOrderDto, UpdateOrderDto, PatchOrderDto> service)
    : CrudController<OrderDto, CreateOrderDto, UpdateOrderDto, PatchOrderDto>(service);
