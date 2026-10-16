using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/payment-methods")]
public class PaymentMethodsController(IService<PaymentMethodDto, CreatePaymentMethodDto, UpdatePaymentMethodDto, PatchPaymentMethodDto> service)
    : CrudController<PaymentMethodDto, CreatePaymentMethodDto, UpdatePaymentMethodDto, PatchPaymentMethodDto>(service);
