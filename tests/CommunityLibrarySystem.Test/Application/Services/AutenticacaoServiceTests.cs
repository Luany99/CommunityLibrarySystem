using CommunityLibrarySystem.Application.DTOs;
using CommunityLibrarySystem.Application.Interfaces;
using CommunityLibrarySystem.Application.Services;
using CommunityLibrarySystem.Domain.Entities;
using CommunityLibrarySystem.Domain.Repositories;
using CommunityLibrarySystem.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using Xunit;

namespace CommunityLibrarySystem.Test.Application.Services
{
    public class AutenticacaoServiceTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly Mock<IJwtTokenService> _jwtTokenServiceMock;
        private readonly AutenticacaoService _autenticacaoService;

        public AutenticacaoServiceTests()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _jwtTokenServiceMock = new Mock<IJwtTokenService>();
            _autenticacaoService = new AutenticacaoService(
                _usuarioRepositoryMock.Object,
                _jwtTokenServiceMock.Object
            );
        }

        [Fact]
        public void Registrar_DeveRegistrarUsuarioComSucesso_QuandoDadosValidos()
        {
            // Arrange
            var dto = new RegistrarUsuarioDto
            {
                Nome = "João Silva",
                Email = "joao@email.com",
                Senha = "Senha@123"
            };

            _usuarioRepositoryMock
                .Setup(x => x.ObterPorEmail(dto.Email))
                .Returns((Usuario)null!);

            _usuarioRepositoryMock
                .Setup(x => x.Adicionar(It.IsAny<Usuario>()));

            _jwtTokenServiceMock
                .Setup(x => x.GerarToken(It.IsAny<Usuario>()))
                .Returns("token-jwt-fake");

            // Act
            var token = _autenticacaoService.Registrar(dto);

            // Assert
            token.Should().Be("token-jwt-fake");
            _usuarioRepositoryMock.Verify(x => x.ObterPorEmail(dto.Email), Times.Once);
            _usuarioRepositoryMock.Verify(x => x.Adicionar(It.IsAny<Usuario>()), Times.Once);
            _jwtTokenServiceMock.Verify(x => x.GerarToken(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public void Registrar_DeveLancarExcecao_QuandoEmailJaCadastrado()
        {
            // Arrange
            var dto = new RegistrarUsuarioDto
            {
                Nome = "João Silva",
                Email = "joao@email.com",
                Senha = "Senha@123"
            };

            var usuarioExistente = new Usuario(
                "Outro Usuário",
                new Email(dto.Email),
                Senha.Criar("OutraSenha@123")
            );

            _usuarioRepositoryMock
                .Setup(x => x.ObterPorEmail(dto.Email))
                .Returns(usuarioExistente);

            // Act
            var act = () => _autenticacaoService.Registrar(dto);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("E-mail já cadastrado");

            _usuarioRepositoryMock.Verify(x => x.ObterPorEmail(dto.Email), Times.Once);
            _usuarioRepositoryMock.Verify(x => x.Adicionar(It.IsAny<Usuario>()), Times.Never);
            _jwtTokenServiceMock.Verify(x => x.GerarToken(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public void Autenticar_DeveRetornarToken_QuandoCredenciaisValidas()
        {
            // Arrange
            var email = "joao@email.com";
            var senha = "Senha@123";
            var usuario = new Usuario("João Silva", new Email(email), Senha.Criar(senha));

            _usuarioRepositoryMock
                .Setup(x => x.ObterPorEmail(email))
                .Returns(usuario);

            _jwtTokenServiceMock
                .Setup(x => x.GerarToken(usuario))
                .Returns("token-jwt-fake");

            // Act
            var token = _autenticacaoService.Autenticar(email, senha);

            // Assert
            token.Should().Be("token-jwt-fake");
            _usuarioRepositoryMock.Verify(x => x.ObterPorEmail(email), Times.Once);
            _jwtTokenServiceMock.Verify(x => x.GerarToken(usuario), Times.Once);
        }

        [Fact]
        public void Autenticar_DeveLancarExcecao_QuandoUsuarioNaoEncontrado()
        {
            // Arrange
            var email = "naoexiste@email.com";
            var senha = "Senha@123";

            _usuarioRepositoryMock
                .Setup(x => x.ObterPorEmail(email))
                .Returns((Usuario)null!);

            // Act
            var act = () => _autenticacaoService.Autenticar(email, senha);

            // Assert
            act.Should().Throw<UnauthorizedAccessException>()
                .WithMessage("Usuário ou senha inválidos");

            _usuarioRepositoryMock.Verify(x => x.ObterPorEmail(email), Times.Once);
            _jwtTokenServiceMock.Verify(x => x.GerarToken(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public void Autenticar_DeveLancarExcecao_QuandoSenhaIncorreta()
        {
            // Arrange
            var email = "joao@email.com";
            var senhaCorreta = "Senha@123";
            var senhaIncorreta = "SenhaErrada@123";
            var usuario = new Usuario("João Silva", new Email(email), Senha.Criar(senhaCorreta));

            _usuarioRepositoryMock
                .Setup(x => x.ObterPorEmail(email))
                .Returns(usuario);

            // Act
            var act = () => _autenticacaoService.Autenticar(email, senhaIncorreta);

            // Assert
            act.Should().Throw<UnauthorizedAccessException>()
                .WithMessage("Usuário ou senha inválidos");

            _usuarioRepositoryMock.Verify(x => x.ObterPorEmail(email), Times.Once);
            _jwtTokenServiceMock.Verify(x => x.GerarToken(It.IsAny<Usuario>()), Times.Never);
        }
    }
}
