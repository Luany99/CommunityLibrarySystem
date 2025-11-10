using CommunityLibrarySystem.Domain.Entities;
using CommunityLibrarySystem.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CommunityLibrarySystem.Test.Domain.Entities
{
    public class UsuarioTests
    {
        [Fact]
        public void Construtor_DevecriarUsuarioValido_QuandoDadosEstaoCorretos()
        {
            // Arrange
            var nome = "João Silva";
            var email = new Email("joao@email.com");
            var senha = Senha.Criar("Senha@123");

            // Act
            var usuario = new Usuario(nome, email, senha);

            // Assert
            usuario.Should().NotBeNull();
            usuario.Nome.Should().Be(nome);
            usuario.Email.Should().Be(email);
            usuario.SenhaHash.Should().Be(senha);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Construtor_DeveLancarExcecao_QuandoNomeInvalido(string nomeInvalido)
        {
            // Arrange
            var email = new Email("joao@email.com");
            var senha = Senha.Criar("Senha@123");

            // Act
            var act = () => new Usuario(nomeInvalido, email, senha);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Nome obrigatório");
        }

        [Fact]
        public void Construtor_DeveLancarExcecao_QuandoNomeNulo()
        {
            // Arrange
            var email = new Email("joao@email.com");
            var senha = Senha.Criar("Senha@123");

            // Act
            var act = () => new Usuario(null!, email, senha);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Nome obrigatório");
        }

        [Fact]
        public void Construtor_DeveLancarExcecao_QuandoEmailNulo()
        {
            // Arrange
            var nome = "João Silva";
            var senha = Senha.Criar("Senha@123");

            // Act
            var act = () => new Usuario(nome, null!, senha);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("E-mail obrigatório");
        }

        [Fact]
        public void Construtor_DeveLancarExcecao_QuandoSenhaNula()
        {
            // Arrange
            var nome = "João Silva";
            var email = new Email("joao@email.com");

            // Act
            var act = () => new Usuario(nome, email, null!);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Senha obrigatória");
        }

        [Fact]
        public void VerificarSenha_DeveRetornarTrue_QuandoSenhaCorreta()
        {
            // Arrange
            var senhaTexto = "Senha@123";
            var usuario = new Usuario("João Silva", new Email("joao@email.com"), Senha.Criar(senhaTexto));

            // Act
            var resultado = usuario.VerificarSenha(senhaTexto);

            // Assert
            resultado.Should().BeTrue();
        }

        [Fact]
        public void VerificarSenha_DeveRetornarFalse_QuandoSenhaIncorreta()
        {
            // Arrange
            var usuario = new Usuario("João Silva", new Email("joao@email.com"), Senha.Criar("Senha@123"));

            // Act
            var resultado = usuario.VerificarSenha("SenhaErrada@123");

            // Assert
            resultado.Should().BeFalse();
        }
    }
}
