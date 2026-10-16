using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/sizes")]
public class SizesController(IService<SizeDto, CreateSizeDto, UpdateSizeDto, PatchSizeDto> service)
    : CrudController<SizeDto, CreateSizeDto, UpdateSizeDto, PatchSizeDto>(service);
