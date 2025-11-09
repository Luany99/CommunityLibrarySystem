using CommunityLibrarySystem.Domain.Entities;

namespace CommunityLibrarySystem.Domain.Repositories
{
    public interface IEmprestimoRepository
    {
        Emprestimo Adicionar(Emprestimo emprestimo);
        Emprestimo ObterPorId(int id);
        IEnumerable<Emprestimo> Listar();
        (IEnumerable<Emprestimo> Items, int Total) ListarPaginado(int page, int pageSize);
        void Atualizar(Emprestimo emprestimo);
    }
}
