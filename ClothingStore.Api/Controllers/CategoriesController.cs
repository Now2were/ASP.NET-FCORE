using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/categories")]
public class CategoriesController(IService<CategoryDto, CreateCategoryDto, UpdateCategoryDto, PatchCategoryDto> service)
    : CrudController<CategoryDto, CreateCategoryDto, UpdateCategoryDto, PatchCategoryDto>(service);
