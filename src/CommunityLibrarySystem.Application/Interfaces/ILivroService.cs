using CommunityLibrarySystem.Application.DTOs;
using CommunityLibrarySystem.Domain.Entities;

namespace CommunityLibrarySystem.Application.Interfaces
{
    public interface ILivroService
    {
        Livro CriarLivro(LivroDto dto);
        Livro ObterPorId(int id);
        IEnumerable<Livro> Listar();
        PagedResult<Livro> ListarPaginado(int page, int pageSize);
    }
}
