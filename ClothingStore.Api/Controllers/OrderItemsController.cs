using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/order-items")]
public class OrderItemsController(IService<OrderItemDto, CreateOrderItemDto, UpdateOrderItemDto, PatchOrderItemDto> service)
    : CrudController<OrderItemDto, CreateOrderItemDto, UpdateOrderItemDto, PatchOrderItemDto>(service);
