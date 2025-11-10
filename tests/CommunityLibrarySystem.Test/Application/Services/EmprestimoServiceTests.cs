using CommunityLibrarySystem.Application.DTOs;
using CommunityLibrarySystem.Application.Services;
using CommunityLibrarySystem.Domain.Entities;
using CommunityLibrarySystem.Domain.Enums;
using CommunityLibrarySystem.Domain.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace CommunityLibrarySystem.Test.Application.Services
{
    public class EmprestimoServiceTests
    {
        private readonly Mock<ILivroRepository> _livroRepositoryMock;
        private readonly Mock<IEmprestimoRepository> _emprestimoRepositoryMock;
        private readonly EmprestimoService _emprestimoService;

        public EmprestimoServiceTests()
        {
            _livroRepositoryMock = new Mock<ILivroRepository>();
            _emprestimoRepositoryMock = new Mock<IEmprestimoRepository>();
            _emprestimoService = new EmprestimoService(
                _livroRepositoryMock.Object,
                _emprestimoRepositoryMock.Object
            );
        }

        [Fact]
        public void SolicitarEmprestimo_DeveCriarEmprestimoComSucesso_QuandoLivroDisponivel()
        {
            // Arrange
            var dto = new EmprestimoDto { LivroId = 1 };
            var livro = new Livro("Clean Code", "Robert C. Martin", 2008, 5);
            var emprestimoEsperado = new Emprestimo(dto.LivroId);

            _livroRepositoryMock
                .Setup(x => x.ObterPorId(dto.LivroId))
                .Returns(livro);

            _livroRepositoryMock
                .Setup(x => x.Atualizar(It.IsAny<Livro>()));

            _emprestimoRepositoryMock
                .Setup(x => x.Adicionar(It.IsAny<Emprestimo>()))
                .Returns(emprestimoEsperado);

            // Act
            var resultado = _emprestimoService.SolicitarEmprestimo(dto);

            // Assert
            resultado.Should().NotBeNull();
            resultado.LivroId.Should().Be(dto.LivroId);
            resultado.Status.Should().Be(StatusEmprestimo.Ativo);
            _livroRepositoryMock.Verify(x => x.ObterPorId(dto.LivroId), Times.Once);
            _livroRepositoryMock.Verify(x => x.Atualizar(It.IsAny<Livro>()), Times.Once);
            _emprestimoRepositoryMock.Verify(x => x.Adicionar(It.IsAny<Emprestimo>()), Times.Once);
        }

        [Fact]
        public void SolicitarEmprestimo_DeveLancarExcecao_QuandoLivroNaoEncontrado()
        {
            // Arrange
            var dto = new EmprestimoDto { LivroId = 999 };

            _livroRepositoryMock
                .Setup(x => x.ObterPorId(dto.LivroId))
                .Returns((Livro)null!);

            // Act
            var act = () => _emprestimoService.SolicitarEmprestimo(dto);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Livro não encontrado.");

            _livroRepositoryMock.Verify(x => x.ObterPorId(dto.LivroId), Times.Once);
            _livroRepositoryMock.Verify(x => x.Atualizar(It.IsAny<Livro>()), Times.Never);
            _emprestimoRepositoryMock.Verify(x => x.Adicionar(It.IsAny<Emprestimo>()), Times.Never);
        }

        [Fact]
        public void SolicitarEmprestimo_DeveLancarExcecao_QuandoLivroSemExemplares()
        {
            // Arrange
            var dto = new EmprestimoDto { LivroId = 1 };
            var livro = new Livro("Clean Code", "Robert C. Martin", 2008, 0);

            _livroRepositoryMock
                .Setup(x => x.ObterPorId(dto.LivroId))
                .Returns(livro);

            // Act
            var act = () => _emprestimoService.SolicitarEmprestimo(dto);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Não há exemplares disponíveis.");

            _livroRepositoryMock.Verify(x => x.ObterPorId(dto.LivroId), Times.Once);
            _livroRepositoryMock.Verify(x => x.Atualizar(It.IsAny<Livro>()), Times.Never);
            _emprestimoRepositoryMock.Verify(x => x.Adicionar(It.IsAny<Emprestimo>()), Times.Never);
        }

        [Fact]
        public void DevolverEmprestimo_DeveProcessarDevolucaoComSucesso()
        {
            // Arrange
            var emprestimoId = 1;
            var livroId = 1;
            var emprestimo = new Emprestimo(livroId);
            var livro = new Livro("Clean Code", "Robert C. Martin", 2008, 4);

            _emprestimoRepositoryMock
                .Setup(x => x.ObterPorId(emprestimoId))
                .Returns(emprestimo);

            _livroRepositoryMock
                .Setup(x => x.ObterPorId(livroId))
                .Returns(livro);

            _emprestimoRepositoryMock
                .Setup(x => x.Atualizar(It.IsAny<Emprestimo>()));

            _livroRepositoryMock
                .Setup(x => x.Atualizar(It.IsAny<Livro>()));

            // Act
            _emprestimoService.DevolverEmprestimo(emprestimoId);

            // Assert
            _emprestimoRepositoryMock.Verify(x => x.ObterPorId(emprestimoId), Times.Once);
            _livroRepositoryMock.Verify(x => x.ObterPorId(livroId), Times.Once);
            _emprestimoRepositoryMock.Verify(x => x.Atualizar(It.IsAny<Emprestimo>()), Times.Once);
            _livroRepositoryMock.Verify(x => x.Atualizar(It.IsAny<Livro>()), Times.Once);
        }

        [Fact]
        public void DevolverEmprestimo_DeveLancarExcecao_QuandoEmprestimoNaoEncontrado()
        {
            // Arrange
            var emprestimoId = 999;

            _emprestimoRepositoryMock
                .Setup(x => x.ObterPorId(emprestimoId))
                .Returns((Emprestimo)null!);

            // Act
            var act = () => _emprestimoService.DevolverEmprestimo(emprestimoId);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Empréstimo não encontrado.");

            _emprestimoRepositoryMock.Verify(x => x.ObterPorId(emprestimoId), Times.Once);
            _livroRepositoryMock.Verify(x => x.ObterPorId(It.IsAny<int>()), Times.Never);
            _emprestimoRepositoryMock.Verify(x => x.Atualizar(It.IsAny<Emprestimo>()), Times.Never);
            _livroRepositoryMock.Verify(x => x.Atualizar(It.IsAny<Livro>()), Times.Never);
        }

        [Fact]
        public void DevolverEmprestimo_DeveLancarExcecao_QuandoLivroNaoEncontrado()
        {
            // Arrange
            var emprestimoId = 1;
            var livroId = 1;
            var emprestimo = new Emprestimo(livroId);

            _emprestimoRepositoryMock
                .Setup(x => x.ObterPorId(emprestimoId))
                .Returns(emprestimo);

            _livroRepositoryMock
                .Setup(x => x.ObterPorId(livroId))
                .Returns((Livro)null!);

            // Act
            var act = () => _emprestimoService.DevolverEmprestimo(emprestimoId);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Livro associado ao empréstimo não foi encontrado.");

            _emprestimoRepositoryMock.Verify(x => x.ObterPorId(emprestimoId), Times.Once);
            _livroRepositoryMock.Verify(x => x.ObterPorId(livroId), Times.Once);
            _emprestimoRepositoryMock.Verify(x => x.Atualizar(It.IsAny<Emprestimo>()), Times.Never);
            _livroRepositoryMock.Verify(x => x.Atualizar(It.IsAny<Livro>()), Times.Never);
        }

        [Fact]
        public void ObterPorId_DeveRetornarEmprestimo_QuandoEmprestimoExiste()
        {
            // Arrange
            var emprestimoId = 1;
            var emprestimoEsperado = new Emprestimo(1);

            _emprestimoRepositoryMock
                .Setup(x => x.ObterPorId(emprestimoId))
                .Returns(emprestimoEsperado);

            // Act
            var resultado = _emprestimoService.ObterPorId(emprestimoId);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().Be(emprestimoEsperado);
            _emprestimoRepositoryMock.Verify(x => x.ObterPorId(emprestimoId), Times.Once);
        }

        [Fact]
        public void ObterPorId_DeveRetornarNull_QuandoEmprestimoNaoExiste()
        {
            // Arrange
            var emprestimoId = 999;

            _emprestimoRepositoryMock
                .Setup(x => x.ObterPorId(emprestimoId))
                .Returns((Emprestimo)null!);

            // Act
            var resultado = _emprestimoService.ObterPorId(emprestimoId);

            // Assert
            resultado.Should().BeNull();
            _emprestimoRepositoryMock.Verify(x => x.ObterPorId(emprestimoId), Times.Once);
        }

        [Fact]
        public void Listar_DeveRetornarTodosOsEmprestimos()
        {
            // Arrange
            var emprestimosEsperados = new List<Emprestimo>
            {
                new Emprestimo(1),
                new Emprestimo(2),
                new Emprestimo(3)
            };

            _emprestimoRepositoryMock
                .Setup(x => x.Listar())
                .Returns(emprestimosEsperados);

            // Act
            var resultado = _emprestimoService.Listar();

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(3);
            resultado.Should().BeEquivalentTo(emprestimosEsperados);
            _emprestimoRepositoryMock.Verify(x => x.Listar(), Times.Once);
        }

        [Fact]
        public void Listar_DeveRetornarListaVazia_QuandoNaoHaEmprestimos()
        {
            // Arrange
            _emprestimoRepositoryMock
                .Setup(x => x.Listar())
                .Returns(new List<Emprestimo>());

            // Act
            var resultado = _emprestimoService.Listar();

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().BeEmpty();
            _emprestimoRepositoryMock.Verify(x => x.Listar(), Times.Once);
        }

        [Fact]
        public void ListarPaginado_DeveRetornarPaginaCorreta()
        {
            // Arrange
            var page = 1;
            var pageSize = 2;
            var emprestimos = new List<Emprestimo>
            {
                new Emprestimo(1),
                new Emprestimo(2)
            };
            var totalItems = 5;

            _emprestimoRepositoryMock
                .Setup(x => x.ListarPaginado(page, pageSize))
                .Returns((emprestimos, totalItems));

            // Act
            var resultado = _emprestimoService.ListarPaginado(page, pageSize);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Items.Should().HaveCount(2);
            resultado.TotalItems.Should().Be(totalItems);
            resultado.Page.Should().Be(page);
            resultado.PageSize.Should().Be(pageSize);
            _emprestimoRepositoryMock.Verify(x => x.ListarPaginado(page, pageSize), Times.Once);
        }

        [Fact]
        public void ListarPaginado_DeveRetornarPaginaVazia_QuandoNaoHaDados()
        {
            // Arrange
            var page = 1;
            var pageSize = 10;

            _emprestimoRepositoryMock
                .Setup(x => x.ListarPaginado(page, pageSize))
                .Returns((new List<Emprestimo>(), 0));

            // Act
            var resultado = _emprestimoService.ListarPaginado(page, pageSize);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Items.Should().BeEmpty();
            resultado.TotalItems.Should().Be(0);
            resultado.Page.Should().Be(page);
            resultado.PageSize.Should().Be(pageSize);
            _emprestimoRepositoryMock.Verify(x => x.ListarPaginado(page, pageSize), Times.Once);
        }
    }
}
