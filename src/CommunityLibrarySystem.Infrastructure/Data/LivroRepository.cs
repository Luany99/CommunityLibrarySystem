using CommunityLibrarySystem.Domain.Entities;
using CommunityLibrarySystem.Domain.Repositories;
using CommunityLibrarySystem.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CommunityLibrarySystem.Infrastructure.Data
{
    public class LivroRepository : ILivroRepository
    {
        private readonly LibraryDbContext _context;

        public LivroRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public Livro Adicionar(Livro livro)
        {
            _context.Livros.Add(livro);
            _context.SaveChanges();
            return livro;
        }

        public Livro ObterPorId(int id)
        {
            return _context.Livros.Find(id);
        }

        public IEnumerable<Livro> Listar()
        {
            return _context.Livros.AsNoTracking().ToList();
        }

        public (IEnumerable<Livro> Items, int Total) ListarPaginado(int page, int pageSize)
        {
            return _context.Livros.AsNoTracking().Paginar(page, pageSize);
        }

        public void Atualizar(Livro livro)
        {
            _context.Livros.Update(livro);
            _context.SaveChanges();
        }
    }
}
