using CommunityLibrarySystem.Domain.Entities;
using CommunityLibrarySystem.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace CommunityLibrarySystem.Test.Domain.Entities
{
    public class EmprestimoTests
    {
        [Fact]
        public void Construtor_DeveCriarEmprestimoValido_ComStatusAtivo()
        {
            // Arrange
            var livroId = 1;

            // Act
            var emprestimo = new Emprestimo(livroId);

            // Assert
            emprestimo.Should().NotBeNull();
            emprestimo.LivroId.Should().Be(livroId);
            emprestimo.Status.Should().Be(StatusEmprestimo.Ativo);
            emprestimo.DataEmprestimo.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
            emprestimo.DataDevolucao.Should().BeNull();
        }

        [Fact]
        public void Devolver_DeveAtualizarStatusEDataDevolucao_QuandoEmprestimoAtivo()
        {
            // Arrange
            var emprestimo = new Emprestimo(1);

            // Act
            emprestimo.Devolver();

            // Assert
            emprestimo.Status.Should().Be(StatusEmprestimo.Devolvido);
            emprestimo.DataDevolucao.Should().NotBeNull();
            emprestimo.DataDevolucao.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        }

        [Fact]
        public void Devolver_DeveLancarExcecao_QuandoEmprestimoJaDevolvido()
        {
            // Arrange
            var emprestimo = new Emprestimo(1);
            emprestimo.Devolver();

            // Act
            var act = () => emprestimo.Devolver();

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Empréstimo já devolvido.");
        }

        [Fact]
        public void Devolver_NaoDeveAlterarDataEmprestimo()
        {
            // Arrange
            var emprestimo = new Emprestimo(1);
            var dataEmprestimoOriginal = emprestimo.DataEmprestimo;

            // Act
            emprestimo.Devolver();

            // Assert
            emprestimo.DataEmprestimo.Should().Be(dataEmprestimoOriginal);
        }
    }
}
