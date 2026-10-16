using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/payment-statuses")]
public class PaymentStatusesController(IService<PaymentStatusDto, CreatePaymentStatusDto, UpdatePaymentStatusDto, PatchPaymentStatusDto> service)
    : CrudController<PaymentStatusDto, CreatePaymentStatusDto, UpdatePaymentStatusDto, PatchPaymentStatusDto>(service);
