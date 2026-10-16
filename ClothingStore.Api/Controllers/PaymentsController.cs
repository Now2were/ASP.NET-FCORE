using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/payments")]
public class PaymentsController(IService<PaymentDto, CreatePaymentDto, UpdatePaymentDto, PatchPaymentDto> service)
    : CrudController<PaymentDto, CreatePaymentDto, UpdatePaymentDto, PatchPaymentDto>(service);
