using CommunityLibrarySystem.Application.DTOs;
using CommunityLibrarySystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommunityLibrarySystem.Api.Controllers
{
    [ApiController]
    [Route("api/emprestimos")]
    public class EmprestimosController : ControllerBase
    {
        private readonly IEmprestimoService _emprestimoService;

        public EmprestimosController(IEmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        [HttpPost]
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

        [HttpPost("{id}/devolver")]
        public IActionResult DevolverEmprestimo(Guid id)
        {
            try
            {
                _emprestimoService.DevolverEmprestimo(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            var emprestimo = _emprestimoService.ObterPorId(id);
            if (emprestimo == null) return NotFound();
            return Ok(emprestimo);
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var emprestimos = _emprestimoService.Listar();
            return Ok(emprestimos);
        }
    }
}
