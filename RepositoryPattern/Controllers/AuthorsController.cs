using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Core;
using RepositoryPattern.Core.Interfaces;
using RepositoryPattern.Core.Models;

namespace RepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            return Ok(_unitOfWork.Authors.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult getById([FromRoute]int id)
        {
            return Ok(_unitOfWork.Authors.Get(id));
        }
        [HttpGet("getbyname/{name}")]
        public async Task<IActionResult> getByName([FromRoute] string name)
        {
            var result = await _unitOfWork.Authors.FindAsync(a => a.Name == name);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
