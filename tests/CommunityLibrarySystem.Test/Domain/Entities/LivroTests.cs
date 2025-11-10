using CommunityLibrarySystem.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace CommunityLibrarySystem.Test.Domain.Entities
{
    public class LivroTests
    {
        [Fact]
        public void Construtor_DeveCriarLivroValido_QuandoDadosEstaoCorretos()
        {
            // Arrange
            var titulo = "Clean Code";
            var autor = "Robert C. Martin";
            var anoPublicacao = 2008;
            var quantidadeDisponivel = 5;

            // Act
            var livro = new Livro(titulo, autor, anoPublicacao, quantidadeDisponivel);

            // Assert
            livro.Should().NotBeNull();
            livro.Titulo.Should().Be(titulo);
            livro.Autor.Should().Be(autor);
            livro.AnoPublicacao.Should().Be(anoPublicacao);
            livro.QuantidadeDisponivel.Should().Be(quantidadeDisponivel);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Construtor_DeveLancarExcecao_QuandoTituloInvalido(string tituloInvalido)
        {
            // Arrange & Act
            var act = () => new Livro(tituloInvalido, "Autor Teste", 2020, 5);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Título é obrigatório.");
        }

        [Fact]
        public void Construtor_DeveLancarExcecao_QuandoTituloNulo()
        {
            // Arrange & Act
            var act = () => new Livro(null!, "Autor Teste", 2020, 5);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Título é obrigatório.");
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Construtor_DeveLancarExcecao_QuandoAutorInvalido(string autorInvalido)
        {
            // Arrange & Act
            var act = () => new Livro("Livro Teste", autorInvalido, 2020, 5);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Autor é obrigatório.");
        }

        [Fact]
        public void Construtor_DeveLancarExcecao_QuandoAutorNulo()
        {
            // Arrange & Act
            var act = () => new Livro("Livro Teste", null!, 2020, 5);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Autor é obrigatório.");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Construtor_DeveLancarExcecao_QuandoQuantidadeNegativa(int quantidadeInvalida)
        {
            // Arrange & Act
            var act = () => new Livro("Livro Teste", "Autor Teste", 2020, quantidadeInvalida);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Quantidade disponível não pode ser negativa.");
        }

        [Fact]
        public void ReduzirQuantidade_DeveDecrementarQuantidade_QuandoHaExemplares()
        {
            // Arrange
            var livro = new Livro("Clean Code", "Robert C. Martin", 2008, 5);
            var quantidadeInicial = livro.QuantidadeDisponivel;

            // Act
            livro.ReduzirQuantidade();

            // Assert
            livro.QuantidadeDisponivel.Should().Be(quantidadeInicial - 1);
        }

        [Fact]
        public void ReduzirQuantidade_DeveLancarExcecao_QuandoNaoHaExemplares()
        {
            // Arrange
            var livro = new Livro("Clean Code", "Robert C. Martin", 2008, 0);

            // Act
            var act = () => livro.ReduzirQuantidade();

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Não há exemplares disponíveis.");
        }

        [Fact]
        public void AumentarQuantidade_DeveIncrementarQuantidade()
        {
            // Arrange
            var livro = new Livro("Clean Code", "Robert C. Martin", 2008, 5);
            var quantidadeInicial = livro.QuantidadeDisponivel;

            // Act
            livro.AumentarQuantidade();

            // Assert
            livro.QuantidadeDisponivel.Should().Be(quantidadeInicial + 1);
        }

        [Fact]
        public void AumentarQuantidade_DeveIncrementar_MesmoQuandoQuantidadeZero()
        {
            // Arrange
            var livro = new Livro("Clean Code", "Robert C. Martin", 2008, 0);

            // Act
            livro.AumentarQuantidade();

            // Assert
            livro.QuantidadeDisponivel.Should().Be(1);
        }
    }
}
