using CommunityLibrarySystem.Application.DTOs;
using CommunityLibrarySystem.Domain.Entities;

namespace CommunityLibrarySystem.Application.Interfaces
{
    public interface ILivroService
    {
        Livro CriarLivro(LivroDto dto);
        Livro ObterPorId(Guid id);
        IEnumerable<Livro> Listar();
    }
}
