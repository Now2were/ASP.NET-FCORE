using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/product-variants")]
public class ProductVariantsController(IService<ProductVariantDto, CreateProductVariantDto, UpdateProductVariantDto, PatchProductVariantDto> service)
    : CrudController<ProductVariantDto, CreateProductVariantDto, UpdateProductVariantDto, PatchProductVariantDto>(service);
