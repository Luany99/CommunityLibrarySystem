using CommunityLibrarySystem.Application.DTOs;
using CommunityLibrarySystem.Application.Interfaces;
using CommunityLibrarySystem.Domain.Entities;
using CommunityLibrarySystem.Domain.Repositories;

namespace CommunityLibrarySystem.Application.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public Livro CriarLivro(LivroDto dto)
        {
            var livro = new Livro(dto.Titulo, dto.Autor, dto.AnoPublicacao, dto.QuantidadeDisponivel);
            return _livroRepository.Adicionar(livro);
        }

        public Livro ObterPorId(Guid id)
        {
            return _livroRepository.ObterPorId(id);
        }

        public IEnumerable<Livro> Listar()
        {
            return _livroRepository.Listar();
        }
    }
}
