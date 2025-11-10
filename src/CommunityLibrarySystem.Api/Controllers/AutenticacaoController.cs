using CommunityLibrarySystem.Application.DTOs;
using CommunityLibrarySystem.Application.Interfaces;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller responsável pela autenticação e registro de usuários.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAutenticacaoService _autenticacaoService;

    public AuthController(IAutenticacaoService autenticacaoService)
    {
        _autenticacaoService = autenticacaoService;
    }

    /// <summary>
    /// Realiza login do usuário.
    /// </summary>
    /// <param name="dto">Objeto contendo email e senha do usuário.</param>
    /// <returns>Retorna o token JWT em caso de sucesso.</returns>
    /// <response code="200">Login realizado com sucesso.</response>
    /// <response code="401">Credenciais inválidas.</response>
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        try
        {
            var token = _autenticacaoService.Autenticar(dto.Email, dto.Senha);
            return Ok(new { token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Registra um novo usuário.
    /// </summary>
    /// <param name="dto">Objeto contendo informações do usuário.</param>
    /// <returns>Retorna o token JWT do novo usuário.</returns>
    /// <response code="200">Registro realizado com sucesso.</response>
    /// <response code="400">Dados inválidos.</response>
    [HttpPost("registrar")]
    public IActionResult Registrar([FromBody] RegistrarUsuarioDto dto)
    {
        try
        {
            var token = _autenticacaoService.Registrar(dto);
            return Ok(new { token });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
