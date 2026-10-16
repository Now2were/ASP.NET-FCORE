using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/product-materials")]
public class ProductMaterialsController(IService<ProductMaterialDto, CreateProductMaterialDto, UpdateProductMaterialDto, PatchProductMaterialDto> service)
    : CrudController<ProductMaterialDto, CreateProductMaterialDto, UpdateProductMaterialDto, PatchProductMaterialDto>(service);
