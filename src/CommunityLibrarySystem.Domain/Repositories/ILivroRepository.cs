using CommunityLibrarySystem.Domain.Entities;

namespace CommunityLibrarySystem.Domain.Repositories
{
    public interface ILivroRepository
    {
        Livro Adicionar(Livro livro);
        Livro ObterPorId(Guid id);
        IEnumerable<Livro> Listar();
        void Atualizar(Livro livro);
    }
}
