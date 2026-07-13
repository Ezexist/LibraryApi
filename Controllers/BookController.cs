using Library.Data;
using Library.DTOs.Book;
using Library.Interfaces;
using Library.Mappers;
using Library.QueryObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        private readonly IBookRepository _bookRepo;
        public BookController(LibraryDbContext context, IBookRepository bookRepository)
        {
            _bookRepo = bookRepository;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BookQueryObject query)
        {
            var books = await _bookRepo.GetAllAsync(query);
            return Ok(books.Select(b => b.ToBookDto()));
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);

            return Ok(book.ToBookDto());
        }
        [HttpGet("by-author/{id:int}")]
        public async Task<IActionResult> GetByAuthor([FromRoute] int id)
        {
            var books = await _bookRepo.GetByAuthorAsync(id);

            return Ok(books.Select(b => b.ToBookDto()));
        }
        [HttpGet("top-rated")]
        public async Task<IActionResult> GetByAvarageRating([FromQuery] float rating)
        {
            var books = await _bookRepo.GetTopRatedAsync(rating);

            //ToListAsync never bring null so it doesnt make sensce to check if(null)

            return Ok(books.Select(b => b.ToBookDto()));
        }
        [HttpGet("most-popular")]
        public async Task<IActionResult> GetMostPopular([FromQuery] int count)
        {
            var books = await _bookRepo.GetMostReviewedAsync(count);
            return Ok(books.Select(b => b.ToBookDto()));
        }
        [HttpGet("genre-statistic")]
        public async Task<IActionResult> GetGenreStatistic()
        {
            var statistic = await _bookRepo.GetGenreStatistic();
            return Ok(statistic);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookDto createBookDto) 
        {
            var bookModel = createBookDto.ToBookFromCreatedDto();
            var createdBook = await _bookRepo.CreateAsync(bookModel);
            if (createdBook == null)
                return BadRequest("Author does not exist.");
            return CreatedAtAction(nameof(GetById),
                new { id = createdBook.Id }, createdBook.ToBookDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,
            [FromBody] UpdateBookDto updateBookDto)
        {
            var bookModel = await _bookRepo.UpdateAsync(id, updateBookDto);
            if(bookModel == null)
                return NotFound();
            return Ok(bookModel.ToBookDto());
        }
    }
}
