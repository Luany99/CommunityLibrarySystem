using CommunityLibrarySystem.Application.DTOs;
using CommunityLibrarySystem.Application.Interfaces;
using CommunityLibrarySystem.Domain.Entities;
using CommunityLibrarySystem.Domain.Repositories;
using CommunityLibrarySystem.Domain.ValueObjects;

namespace CommunityLibrarySystem.Application.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public AutenticacaoService(IUsuarioRepository usuarioRepository, IJwtTokenService jwtTokenService)
        {
            _usuarioRepository = usuarioRepository;
            _jwtTokenService = jwtTokenService;
        }

        public string Registrar(RegistrarUsuarioDto dto)
        {
            if (_usuarioRepository.ObterPorEmail(dto.Email) != null)
                throw new InvalidOperationException("E-mail já cadastrado");

            var usuario = new Usuario(dto.Nome, new Email(dto.Email), Senha.Criar(dto.Senha));
            _usuarioRepository.Adicionar(usuario);

            return _jwtTokenService.GerarToken(usuario);
        }

        public string Autenticar(string email, string senha)
        {
            var usuario = _usuarioRepository.ObterPorEmail(email);
            if (usuario == null || !usuario.VerificarSenha(senha))
                throw new UnauthorizedAccessException("Usuário ou senha inválidos");

            return _jwtTokenService.GerarToken(usuario);
        }
    }
}
