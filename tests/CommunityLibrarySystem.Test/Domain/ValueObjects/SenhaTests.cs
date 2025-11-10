using CommunityLibrarySystem.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CommunityLibrarySystem.Test.Domain.ValueObjects
{
    public class SenhaTests
    {
        [Theory]
        [InlineData("Senha@123")]
        [InlineData("P@ssw0rd")]
        [InlineData("Teste#2024")]
        [InlineData("MinhaSenh@1")]
        public void Criar_DeveCriarSenhaValida_QuandoCriteriosSaoAtendidos(string senhaValida)
        {
            // Act
            var senha = Senha.Criar(senhaValida);

            // Assert
            senha.Should().NotBeNull();
            senha.Hash.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Criar_DeveLancarExcecao_QuandoSenhaVazia(string senhaInvalida)
        {
            // Act
            var act = () => Senha.Criar(senhaInvalida);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Senha obrigatória");
        }

        [Fact]
        public void Criar_DeveLancarExcecao_QuandoSenhaNula()
        {
            // Act
            var act = () => Senha.Criar(null!);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Senha obrigatória");
        }

        [Theory]
        [InlineData("senha")]
        [InlineData("SENHA")]
        [InlineData("Senha123")]
        [InlineData("Senha@")]
        [InlineData("senha@123")]
        [InlineData("SENHA@123")]
        [InlineData("Senha@abc")]
        [InlineData("SenhaAbc123")]
        public void Criar_DeveLancarExcecao_QuandoSenhaNaoAtendeCriterios(string senhaInvalida)
        {
            // Act
            var act = () => Senha.Criar(senhaInvalida);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Senha deve ter pelo menos 8 caracteres, uma letra maiúscula, uma minúscula, um número e um caractere especial.");
        }

        [Fact]
        public void Criar_DeveGerarHashesDiferentes_ParaMesmaSenha()
        {
            // Arrange
            var senhaTexto = "Senha@123";

            // Act
            var senha1 = Senha.Criar(senhaTexto);
            var senha2 = Senha.Criar(senhaTexto);

            // Assert
            senha1.Hash.Should().NotBe(senha2.Hash);
        }

        [Fact]
        public void Verificar_DeveRetornarTrue_QuandoSenhaCorreta()
        {
            // Arrange
            var senhaTexto = "Senha@123";
            var senha = Senha.Criar(senhaTexto);

            // Act
            var resultado = senha.Verificar(senhaTexto);

            // Assert
            resultado.Should().BeTrue();
        }

        [Fact]
        public void Verificar_DeveRetornarFalse_QuandoSenhaIncorreta()
        {
            // Arrange
            var senha = Senha.Criar("Senha@123");

            // Act
            var resultado = senha.Verificar("SenhaErrada@456");

            // Assert
            resultado.Should().BeFalse();
        }

        [Fact]
        public void Reidratar_DeveCriarSenhaComHashExistente()
        {
            // Arrange
            var senhaOriginal = Senha.Criar("Senha@123");
            var hashOriginal = senhaOriginal.Hash;

            // Act
            var senhaReidratada = Senha.Reidratar(hashOriginal);

            // Assert
            senhaReidratada.Should().NotBeNull();
            senhaReidratada.Hash.Should().Be(hashOriginal);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Reidratar_DeveLancarExcecao_QuandoHashVazio(string hashInvalido)
        {
            // Act
            var act = () => Senha.Reidratar(hashInvalido);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Hash inválido");
        }

        [Fact]
        public void Reidratar_DeveLancarExcecao_QuandoHashNulo()
        {
            // Act
            var act = () => Senha.Reidratar(null!);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Hash inválido");
        }
    }
}
