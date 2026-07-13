using Library.DTOs.Review;
using Library.Interfaces;
using Library.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;
        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepo = reviewRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewRepo.GetAllAsync();
            return Ok(reviews.Select(r => r.ToReviewDto()));
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var review = await _reviewRepo.GetByIdAsync(id);
            if (review == null)
                return NotFound();
            return Ok(review.ToReviewDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewDto createReviewDto)
        {
            var reviewModel = createReviewDto.ToReviewFromDto();
            var createReview = await _reviewRepo.CreateAsync(reviewModel);
            if (createReview == null)
                return BadRequest("Book does not exist");
            return CreatedAtAction(nameof(GetById),
                new { id = createReview.Id }, createReview.ToReviewDto());
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Updatep([FromRoute] int id, 
            [FromBody] UpdateReviewDto updateReviewDto)
        {
            var reviewModel = await _reviewRepo.UpdateAsync(id, updateReviewDto);
            if (reviewModel == null)
                return NotFound();
            return Ok(reviewModel.ToReviewDto());
        }
    }
}
