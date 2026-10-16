using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/colors")]
public class ColorsController(IService<ColorDto, CreateColorDto, UpdateColorDto, PatchColorDto> service)
    : CrudController<ColorDto, CreateColorDto, UpdateColorDto, PatchColorDto>(service);
