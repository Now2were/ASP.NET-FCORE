using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/order-statuses")]
public class OrderStatusesController(IService<OrderStatusDto, CreateOrderStatusDto, UpdateOrderStatusDto, PatchOrderStatusDto> service)
    : CrudController<OrderStatusDto, CreateOrderStatusDto, UpdateOrderStatusDto, PatchOrderStatusDto>(service);
