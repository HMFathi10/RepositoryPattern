using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Core;
using RepositoryPattern.Core.Interfaces;
using RepositoryPattern.Core.Models;

namespace RepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            return Ok(_unitOfWork.Books.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult getById([FromRoute] int id)
        {
            return Ok(_unitOfWork.Books.Get(id));
        }
        [HttpGet("getbytitle/{title}")]
        public async Task<IActionResult> getByTitle([FromRoute]string title)
        {
            var result = await _unitOfWork.Books.FindAsync(b => b.Title == title, new[] {"Author"});
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("getallbytitle/{title}")]
        public IActionResult getAllByTitle([FromRoute] string title)
        {
            var result = _unitOfWork.Books.FindAll(b => b.Title == title, new[] { "Author" });
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult addOne([FromBody]Book book)
        {
            return Ok(_unitOfWork.Books.Add(book));
        }
        [HttpPost("AddAll")]
        public IActionResult addAll([FromBody] IEnumerable<Book> books)
        {
            return Ok(_unitOfWork.Books.AddRange(books));
        }
    }
}
