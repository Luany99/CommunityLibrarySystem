using CommunityLibrarySystem.Application.DTOs;
using CommunityLibrarySystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommunityLibrarySystem.Api.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de empréstimos.
    /// </summary>
    [ApiController]
    [Route("api/emprestimos")]
    [Authorize]
    public class EmprestimosController : ControllerBase
    {
        private readonly IEmprestimoService _emprestimoService;

        public EmprestimosController(IEmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        /// <summary>
        /// Solicita um novo empréstimo.
        /// </summary>
        /// <param name="dto">Objeto com os dados do empréstimo.</param>
        /// <returns>Retorna o empréstimo criado.</returns>
        /// <response code="201">Empréstimo criado com sucesso.</response>
        /// <response code="400">Dados inválidos ou erro ao processar a solicitação.</response>
        /// <response code="401">Usuário não autenticado.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SolicitarEmprestimo([FromBody] EmprestimoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var emprestimo = _emprestimoService.SolicitarEmprestimo(dto);
                return CreatedAtAction(nameof(ObterPorId), new { id = emprestimo.Id }, emprestimo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Realiza a devolução de um empréstimo.
        /// </summary>
        /// <param name="id">Id do empréstimo a ser devolvido.</param>
        /// <returns>Retorna status 200 em caso de sucesso.</returns>
        /// <response code="200">Empréstimo devolvido com sucesso.</response>
        /// <response code="400">Erro ao processar a devolução.</response>
        /// <response code="401">Usuário não autenticado.</response>
        [HttpPost("{id}/devolver")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DevolverEmprestimo(int id)
        {
            try
            {
                _emprestimoService.DevolverEmprestimo(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtém um empréstimo pelo seu Id.
        /// </summary>
        /// <param name="id">Id do empréstimo.</param>
        /// <returns>Retorna o empréstimo encontrado.</returns>
        /// <response code="200">Empréstimo encontrado.</response>
        /// <response code="404">Empréstimo não encontrado.</response>
        /// <response code="401">Usuário não autenticado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ObterPorId(int id)
        {
            var emprestimo = _emprestimoService.ObterPorId(id);
            if (emprestimo == null) return NotFound();
            return Ok(emprestimo);
        }

        /// <summary>
        /// Lista todos os empréstimos.
        /// </summary>
        /// <returns>Retorna a lista de todos os empréstimos.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        /// <response code="401">Usuário não autenticado.</response>
        [HttpGet("listar-todos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Listar()
        {
            var emprestimos = _emprestimoService.Listar();
            return Ok(emprestimos);
        }

        /// <summary>
        /// Lista os empréstimos de forma paginada.
        /// </summary>
        /// <param name="page">Página desejada (padrão: 1).</param>
        /// <param name="pageSize">Quantidade de itens por página (padrão: 10).</param>
        /// <returns>Retorna a lista paginada de empréstimos.</returns>
        /// <response code="200">Lista paginada retornada com sucesso.</response>
        /// <response code="401">Usuário não autenticado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Listar([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var emprestimos = _emprestimoService.ListarPaginado(page, pageSize);
            return Ok(emprestimos);
        }
    }
}
