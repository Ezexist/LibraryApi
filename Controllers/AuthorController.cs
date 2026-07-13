using Library.Data;
using Library.DTOs.Author;
using Library.Interfaces;
using Library.Mappers;
using Library.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        private readonly IAuthorRepository _authorRepo;
        public AuthorController(LibraryDbContext context, IAuthorRepository authorRepository)
        {
            _authorRepo = authorRepository;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var authors = await _authorRepo.GetAllAsync();
            return Ok(authors.Select(x => x.ToAuthorDto()));
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var author = await _authorRepo.GetByIdAsync(id);
            if (author == null)
                return NotFound();
            return Ok(author.ToAuthorDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorDto authorDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var authorModel = authorDto.ToAuthorFromCreateDto();
            await _authorRepo.CreateAsync(authorModel);
            return CreatedAtAction(nameof(GetById),
                new { id = authorModel.Id }, authorModel.ToAuthorDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,
            [FromBody] UpdateAuthorDto updateAuthorDto)
        {
            var authorModel = await _authorRepo.UpdateAsync(id, updateAuthorDto);
            if(authorModel == null)
                return NotFound();
            return Ok(authorModel.ToAuthorDto());
        }
    }
}
