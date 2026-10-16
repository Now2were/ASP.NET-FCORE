using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/delivery-methods")]
public class DeliveryMethodsController(IService<DeliveryMethodDto, CreateDeliveryMethodDto, UpdateDeliveryMethodDto, PatchDeliveryMethodDto> service)
    : CrudController<DeliveryMethodDto, CreateDeliveryMethodDto, UpdateDeliveryMethodDto, PatchDeliveryMethodDto>(service);
