using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/product-images")]
public class ProductImagesController(IService<ProductImageDto, CreateProductImageDto, UpdateProductImageDto, PatchProductImageDto> service)
    : CrudController<ProductImageDto, CreateProductImageDto, UpdateProductImageDto, PatchProductImageDto>(service);
