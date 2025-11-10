using CommunityLibrarySystem.Application.DTOs;
using CommunityLibrarySystem.Application.Services;
using CommunityLibrarySystem.Domain.Entities;
using CommunityLibrarySystem.Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace CommunityLibrarySystem.Test.Application.Services
{
    public class LivroServiceTests
    {
        private readonly Mock<ILivroRepository> _livroRepositoryMock;
        private readonly LivroService _livroService;

        public LivroServiceTests()
        {
            _livroRepositoryMock = new Mock<ILivroRepository>();
            _livroService = new LivroService(_livroRepositoryMock.Object);
        }

        [Fact]
        public void CriarLivro_DeveCriarLivroComSucesso_QuandoDadosValidos()
        {
            // Arrange
            var dto = new LivroDto
            {
                Titulo = "Clean Code",
                Autor = "Robert C. Martin",
                AnoPublicacao = 2008,
                QuantidadeDisponivel = 5
            };

            var livroEsperado = new Livro(dto.Titulo, dto.Autor, dto.AnoPublicacao, dto.QuantidadeDisponivel);

            _livroRepositoryMock
                .Setup(x => x.Adicionar(It.IsAny<Livro>()))
                .Returns(livroEsperado);

            // Act
            var resultado = _livroService.CriarLivro(dto);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Titulo.Should().Be(dto.Titulo);
            resultado.Autor.Should().Be(dto.Autor);
            resultado.AnoPublicacao.Should().Be(dto.AnoPublicacao);
            resultado.QuantidadeDisponivel.Should().Be(dto.QuantidadeDisponivel);
            _livroRepositoryMock.Verify(x => x.Adicionar(It.IsAny<Livro>()), Times.Once);
        }

        [Fact]
        public void ObterPorId_DeveRetornarLivro_QuandoLivroExiste()
        {
            // Arrange
            var livroId = 1;
            var livroEsperado = new Livro("Clean Code", "Robert C. Martin", 2008, 5);

            _livroRepositoryMock
                .Setup(x => x.ObterPorId(livroId))
                .Returns(livroEsperado);

            // Act
            var resultado = _livroService.ObterPorId(livroId);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().Be(livroEsperado);
            _livroRepositoryMock.Verify(x => x.ObterPorId(livroId), Times.Once);
        }

        [Fact]
        public void ObterPorId_DeveRetornarNull_QuandoLivroNaoExiste()
        {
            // Arrange
            var livroId = 999;

            _livroRepositoryMock
                .Setup(x => x.ObterPorId(livroId))
                .Returns((Livro)null!);

            // Act
            var resultado = _livroService.ObterPorId(livroId);

            // Assert
            resultado.Should().BeNull();
            _livroRepositoryMock.Verify(x => x.ObterPorId(livroId), Times.Once);
        }

        [Fact]
        public void Listar_DeveRetornarTodosOsLivros()
        {
            // Arrange
            var livrosEsperados = new List<Livro>
            {
                new Livro("Clean Code", "Robert C. Martin", 2008, 5),
                new Livro("The Pragmatic Programmer", "Andrew Hunt", 1999, 3),
                new Livro("Design Patterns", "Gang of Four", 1994, 2)
            };

            _livroRepositoryMock
                .Setup(x => x.Listar())
                .Returns(livrosEsperados);

            // Act
            var resultado = _livroService.Listar();

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(3);
            resultado.Should().BeEquivalentTo(livrosEsperados);
            _livroRepositoryMock.Verify(x => x.Listar(), Times.Once);
        }

        [Fact]
        public void Listar_DeveRetornarListaVazia_QuandoNaoHaLivros()
        {
            // Arrange
            _livroRepositoryMock
                .Setup(x => x.Listar())
                .Returns(new List<Livro>());

            // Act
            var resultado = _livroService.Listar();

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().BeEmpty();
            _livroRepositoryMock.Verify(x => x.Listar(), Times.Once);
        }

        [Fact]
        public void ListarPaginado_DeveRetornarPaginaCorreta()
        {
            // Arrange
            var page = 1;
            var pageSize = 2;
            var livros = new List<Livro>
            {
                new Livro("Clean Code", "Robert C. Martin", 2008, 5),
                new Livro("The Pragmatic Programmer", "Andrew Hunt", 1999, 3)
            };
            var totalItems = 5;

            _livroRepositoryMock
                .Setup(x => x.ListarPaginado(page, pageSize))
                .Returns((livros, totalItems));

            // Act
            var resultado = _livroService.ListarPaginado(page, pageSize);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Items.Should().HaveCount(2);
            resultado.TotalItems.Should().Be(totalItems);
            resultado.Page.Should().Be(page);
            resultado.PageSize.Should().Be(pageSize);
            _livroRepositoryMock.Verify(x => x.ListarPaginado(page, pageSize), Times.Once);
        }

        [Fact]
        public void ListarPaginado_DeveRetornarPaginaVazia_QuandoNaoHaDados()
        {
            // Arrange
            var page = 1;
            var pageSize = 10;

            _livroRepositoryMock
                .Setup(x => x.ListarPaginado(page, pageSize))
                .Returns((new List<Livro>(), 0));

            // Act
            var resultado = _livroService.ListarPaginado(page, pageSize);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Items.Should().BeEmpty();
            resultado.TotalItems.Should().Be(0);
            resultado.Page.Should().Be(page);
            resultado.PageSize.Should().Be(pageSize);
            _livroRepositoryMock.Verify(x => x.ListarPaginado(page, pageSize), Times.Once);
        }
    }
}
