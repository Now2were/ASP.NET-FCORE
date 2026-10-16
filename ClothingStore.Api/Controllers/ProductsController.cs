using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/products")]
public class ProductsController(IService<ProductDto, CreateProductDto, UpdateProductDto, PatchProductDto> service)
    : CrudController<ProductDto, CreateProductDto, UpdateProductDto, PatchProductDto>(service);
