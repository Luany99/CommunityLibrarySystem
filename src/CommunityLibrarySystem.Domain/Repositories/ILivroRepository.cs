using CommunityLibrarySystem.Domain.Entities;

namespace CommunityLibrarySystem.Domain.Repositories
{
    public interface ILivroRepository
    {
        Livro Adicionar(Livro livro);
        Livro ObterPorId(int id);
        IEnumerable<Livro> Listar();
        void Atualizar(Livro livro);
        (IEnumerable<Livro> Items, int Total) ListarPaginado(int page, int pageSize);
    }
}
