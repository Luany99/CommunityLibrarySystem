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
            var livro = _livroRepository.ObterPorId(dto.LivroId);
            if (livro == null)
                throw new ArgumentException("Livro não encontrado.");

            livro.ReduzirQuantidade();
            _livroRepository.Atualizar(livro);

            var emprestimo = new Emprestimo(dto.LivroId);
            return _emprestimoRepository.Adicionar(emprestimo);
        }

        public void DevolverEmprestimo(Guid emprestimoId)
        {
            var emprestimo = _emprestimoRepository.ObterPorId(emprestimoId);
            if (emprestimo == null)
                throw new ArgumentException("Empréstimo não encontrado.");

            emprestimo.Devolver();
            _emprestimoRepository.Atualizar(emprestimo);

            var livro = _livroRepository.ObterPorId(emprestimo.LivroId);
            livro.AumentarQuantidade();
            _livroRepository.Atualizar(livro);
        }

        public Emprestimo ObterPorId(Guid id)
        {
            return _emprestimoRepository.ObterPorId(id);
        }

        public IEnumerable<Emprestimo> Listar()
        {
            return _emprestimoRepository.Listar();
        }
    }
}
