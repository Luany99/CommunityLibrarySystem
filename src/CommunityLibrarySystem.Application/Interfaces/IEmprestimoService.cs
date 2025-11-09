using CommunityLibrarySystem.Application.DTOs;
using CommunityLibrarySystem.Domain.Entities;

namespace CommunityLibrarySystem.Application.Interfaces
{
    public interface IEmprestimoService
    {
        Emprestimo SolicitarEmprestimo(EmprestimoDto dto);
        void DevolverEmprestimo(int emprestimoId);
        Emprestimo ObterPorId(int id);
        IEnumerable<Emprestimo> Listar();
        PagedResult<Emprestimo> ListarPaginado(int page, int pageSize);

    }
}
