using CommunityLibrarySystem.Application.DTOs;
using CommunityLibrarySystem.Domain.Entities;

namespace CommunityLibrarySystem.Application.Interfaces
{
    public interface IEmprestimoService
    {
        Emprestimo SolicitarEmprestimo(EmprestimoDto dto);
        void DevolverEmprestimo(Guid emprestimoId);
        Emprestimo ObterPorId(Guid id);
        IEnumerable<Emprestimo> Listar();
    }
}
