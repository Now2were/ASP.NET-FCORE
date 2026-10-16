using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/brands")]
public class BrandsController(IService<BrandDto, CreateBrandDto, UpdateBrandDto, PatchBrandDto> service)
    : CrudController<BrandDto, CreateBrandDto, UpdateBrandDto, PatchBrandDto>(service);
