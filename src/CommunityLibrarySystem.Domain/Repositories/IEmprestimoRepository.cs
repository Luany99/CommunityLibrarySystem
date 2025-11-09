using CommunityLibrarySystem.Domain.Entities;

namespace CommunityLibrarySystem.Domain.Repositories
{
    public interface IEmprestimoRepository
    {
        Emprestimo Adicionar(Emprestimo emprestimo);
        Emprestimo ObterPorId(Guid id);
        IEnumerable<Emprestimo> Listar();
        void Atualizar(Emprestimo emprestimo);
    }
}
