using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/addresses")]
public class AddressesController(IService<AddressDto, CreateAddressDto, UpdateAddressDto, PatchAddressDto> service)
    : CrudController<AddressDto, CreateAddressDto, UpdateAddressDto, PatchAddressDto>(service);
