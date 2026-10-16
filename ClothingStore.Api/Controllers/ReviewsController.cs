using ClothingStore.Api.DTO;
using ClothingStore.Api.Services;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStore.Api.Controllers;
[Route("api/reviews")]
public class ReviewsController(IService<ReviewDto, CreateReviewDto, UpdateReviewDto, PatchReviewDto> service)
    : CrudController<ReviewDto, CreateReviewDto, UpdateReviewDto, PatchReviewDto>(service);
