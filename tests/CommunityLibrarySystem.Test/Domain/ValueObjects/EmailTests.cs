using CommunityLibrarySystem.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CommunityLibrarySystem.Test.Domain.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData("teste@email.com")]
        [InlineData("usuario.nome@dominio.com.br")]
        [InlineData("email+tag@exemplo.co")]
        [InlineData("a@b.io")]
        public void Construtor_DeveCriarEmailValido_QuandoFormatoCorreto(string emailValido)
        {
            // Act
            var email = new Email(emailValido);

            // Assert
            email.Should().NotBeNull();
            email.Endereco.Should().Be(emailValido);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Construtor_DeveLancarExcecao_QuandoEmailVazio(string emailInvalido)
        {
            // Act
            var act = () => new Email(emailInvalido);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("E-mail obrigatório");
        }

        [Fact]
        public void Construtor_DeveLancarExcecao_QuandoEmailNulo()
        {
            // Act
            var act = () => new Email(null!);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("E-mail obrigatório");
        }

        [Theory]
        [InlineData("emailsemarroba.com")]
        [InlineData("@dominio.com")]
        [InlineData("email@")]
        [InlineData("email@dominio")]
        [InlineData("email@@dominio.com")]
        public void Construtor_DeveLancarExcecao_QuandoFormatoInvalido(string emailInvalido)
        {
            // Act
            var act = () => new Email(emailInvalido);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("E-mail em formato inválido");
        }

        [Fact]
        public void Equals_DeveRetornarTrue_QuandoEmailsIguais()
        {
            // Arrange
            var email1 = new Email("teste@email.com");
            var email2 = new Email("teste@email.com");

            // Act
            var resultado = email1.Equals(email2);

            // Assert
            resultado.Should().BeTrue();
        }

        [Fact]
        public void Equals_DeveRetornarFalse_QuandoEmailsDiferentes()
        {
            // Arrange
            var email1 = new Email("teste1@email.com");
            var email2 = new Email("teste2@email.com");

            // Act
            var resultado = email1.Equals(email2);

            // Assert
            resultado.Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_DeveRetornarHashCodesIguais_ParaEmailsIguais()
        {
            // Arrange
            var email1 = new Email("teste@email.com");
            var email2 = new Email("teste@email.com");

            // Act & Assert
            email1.GetHashCode().Should().Be(email2.GetHashCode());
        }

        [Fact]
        public void ToString_DeveRetornarEndereco()
        {
            // Arrange
            var enderecoEmail = "teste@email.com";
            var email = new Email(enderecoEmail);

            // Act
            var resultado = email.ToString();

            // Assert
            resultado.Should().Be(enderecoEmail);
        }
    }
}
