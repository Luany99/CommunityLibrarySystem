using CommunityLibrarySystem.Domain.Entities;
using CommunityLibrarySystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CommunityLibrarySystem.Infrastructure.Data
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly LibraryDbContext _context;

        public EmprestimoRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public Emprestimo Adicionar(Emprestimo emprestimo)
        {
            _context.Emprestimos.Add(emprestimo);
            _context.SaveChanges();
            return emprestimo;
        }

        public Emprestimo ObterPorId(Guid id)
        {
            return _context.Emprestimos.Find(id);
        }

        public IEnumerable<Emprestimo> Listar()
        {
            return _context.Emprestimos.AsNoTracking().ToList();
        }

        public void Atualizar(Emprestimo emprestimo)
        {
            _context.Emprestimos.Update(emprestimo);
            _context.SaveChanges();
        }
    }
}