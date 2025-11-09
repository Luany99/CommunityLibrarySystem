using CommunityLibrarySystem.Application.DTOs;
using CommunityLibrarySystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommunityLibrarySystem.Api.Controllers
{
    [ApiController]
    [Route("api/livros")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpPost]
        public IActionResult CriarLivro([FromBody] LivroDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var livro = _livroService.CriarLivro(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = livro.Id }, livro);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var livro = _livroService.ObterPorId(id);
            if (livro == null) return NotFound();
            return Ok(livro);
        }

        [HttpGet("listar-todos")]
        public IActionResult Listar()
        {
            var livros = _livroService.Listar();
            return Ok(livros);
        }

        [HttpGet]
        public IActionResult Listar([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var livros = _livroService.ListarPaginado(page, pageSize);
            return Ok(livros);
        }
    }
}
