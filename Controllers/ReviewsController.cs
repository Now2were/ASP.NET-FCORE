using ClothingStore.Api.Data;
using ClothingStore.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Api.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly EntityApiContext _context;

    public ReviewsController(EntityApiContext context)
    {
        _context = context;
    }

    // GET: api/reviews?page=1&pageSize=10
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ReviewDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var reviews = await _context.Reviews
            .OrderBy(review => review.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return reviews.Select(ReviewMapper.ToDto).ToList();
    }

    // GET: api/reviews/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReviewDto>> GetById(int id)
    {
        var review = await _context.Reviews
            .FirstOrDefaultAsync(review => review.Id == id);

        if (review is null)
        {
            return NotFound();
        }

        return ReviewMapper.ToDto(review);
    }

    // POST: api/reviews
    [HttpPost]
    [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReviewDto>> Create(CreateReviewDto dto)
    {
        var review = ReviewMapper.ToEntity(dto);
        review.CreatedAt = DateTime.UtcNow;

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = review.Id }, ReviewMapper.ToDto(review));
    }

    // PUT: api/reviews/5 — полное обновление
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, UpdateReviewDto dto)
    {
        var review = await _context.Reviews
            .FirstOrDefaultAsync(review => review.Id == id);

        if (review is null)
        {
            return NotFound();
        }

        review.ProductId = dto.ProductId;
        review.UserId = dto.UserId;
        review.Rating = dto.Rating;
        review.Title = dto.Title;
        review.Comment = dto.Comment;
        review.IsRecommended = dto.IsRecommended;
        review.IsModerated = dto.IsModerated;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // PATCH: api/reviews/5 — частичное обновление (только переданные поля)
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(int id, PatchReviewDto dto)
    {
        var review = await _context.Reviews
            .FirstOrDefaultAsync(review => review.Id == id);

        if (review is null)
        {
            return NotFound();
        }

        var hasChanges = false;

        if (dto.ProductId is not null)
        {
            review.ProductId = dto.ProductId.Value;
            hasChanges = true;
        }

        if (dto.UserId is not null)
        {
            review.UserId = dto.UserId.Value;
            hasChanges = true;
        }

        if (dto.Rating is not null)
        {
            review.Rating = dto.Rating.Value;
            hasChanges = true;
        }

        if (dto.Title is not null)
        {
            review.Title = dto.Title;
            hasChanges = true;
        }

        if (dto.Comment is not null)
        {
            review.Comment = dto.Comment;
            hasChanges = true;
        }

        if (dto.IsRecommended is not null)
        {
            review.IsRecommended = dto.IsRecommended.Value;
            hasChanges = true;
        }

        if (dto.IsModerated is not null)
        {
            review.IsModerated = dto.IsModerated.Value;
            hasChanges = true;
        }

        if (hasChanges)
        {
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/reviews/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var review = await _context.Reviews
            .FirstOrDefaultAsync(review => review.Id == id);

        if (review is null)
        {
            return NotFound();
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}