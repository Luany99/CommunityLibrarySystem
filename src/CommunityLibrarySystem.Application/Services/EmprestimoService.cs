using CommunityLibrarySystem.Application.DTOs;
using CommunityLibrarySystem.Application.Interfaces;
using CommunityLibrarySystem.Domain.Entities;
using CommunityLibrarySystem.Domain.Repositories;

namespace CommunityLibrarySystem.Application.Services
{
    public class EmprestimoService : IEmprestimoService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IEmprestimoRepository _emprestimoRepository;

        public EmprestimoService(ILivroRepository livroRepository, IEmprestimoRepository emprestimoRepository)
        {
            _livroRepository = livroRepository;
            _emprestimoRepository = emprestimoRepository;
        }

        public Emprestimo SolicitarEmprestimo(EmprestimoDto dto)
        {
            var livro = _livroRepository.ObterPorId(dto.LivroId)
                ?? throw new ArgumentException("Livro não encontrado.");

            livro.ReduzirQuantidade();
            _livroRepository.Atualizar(livro);

            var emprestimo = new Emprestimo(dto.LivroId);
            return _emprestimoRepository.Adicionar(emprestimo);
        }

        public void DevolverEmprestimo(int emprestimoId)
        {
            var emprestimo = _emprestimoRepository.ObterPorId(emprestimoId)
                ?? throw new InvalidOperationException("Empréstimo não encontrado.");

            var livro = _livroRepository.ObterPorId(emprestimo.LivroId)
                ?? throw new InvalidOperationException("Livro associado ao empréstimo não foi encontrado.");

            emprestimo.Devolver();
            livro.AumentarQuantidade();

            _emprestimoRepository.Atualizar(emprestimo);
            _livroRepository.Atualizar(livro);
        }


        public Emprestimo ObterPorId(int id)
        {
            return _emprestimoRepository.ObterPorId(id);
        }

        public IEnumerable<Emprestimo> Listar()
        {
            return _emprestimoRepository.Listar();
        }

        public PagedResult<Emprestimo> ListarPaginado(int page, int pageSize)
        {
            var (items, total) = _emprestimoRepository.ListarPaginado(page, pageSize);
            return new PagedResult<Emprestimo>
            {
                Items = items,
                TotalItems = total,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
