using CommunityLibrarySystem.Application.DTOs;
using CommunityLibrarySystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommunityLibrarySystem.Api.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de livros.
    /// </summary>
    [ApiController]
    [Route("api/livros")]
    [Authorize]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        /// <summary>
        /// Cria um novo livro.
        /// </summary>
        /// <param name="dto">Objeto contendo os dados do livro.</param>
        /// <returns>Retorna o livro criado.</returns>
        /// <response code="201">Livro criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="401">Usuário não autenticado.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CriarLivro([FromBody] LivroDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var livro = _livroService.CriarLivro(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = livro.Id }, livro);
        }

        /// <summary>
        /// Obtém um livro pelo seu Id.
        /// </summary>
        /// <param name="id">Id do livro.</param>
        /// <returns>Retorna o livro encontrado.</returns>
        /// <response code="200">Livro encontrado.</response>
        /// <response code="404">Livro não encontrado.</response>
        /// <response code="401">Usuário não autenticado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ObterPorId(int id)
        {
            var livro = _livroService.ObterPorId(id);
            if (livro == null) return NotFound();
            return Ok(livro);
        }

        /// <summary>
        /// Lista todos os livros.
        /// </summary>
        /// <returns>Retorna a lista de todos os livros.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        /// <response code="401">Usuário não autenticado.</response>
        [HttpGet("listar-todos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Listar()
        {
            var livros = _livroService.Listar();
            return Ok(livros);
        }

        /// <summary>
        /// Lista os livros de forma paginada.
        /// </summary>
        /// <param name="page">Página desejada (padrão: 1).</param>
        /// <param name="pageSize">Quantidade de itens por página (padrão: 10).</param>
        /// <returns>Retorna a lista paginada de livros.</returns>
        /// <response code="200">Lista paginada retornada com sucesso.</response>
        /// <response code="401">Usuário não autenticado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Listar([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var livros = _livroService.ListarPaginado(page, pageSize);
            return Ok(livros);
        }
    }
}
