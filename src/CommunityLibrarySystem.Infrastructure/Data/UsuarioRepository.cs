using CommunityLibrarySystem.Domain.Entities;
using CommunityLibrarySystem.Domain.Repositories;
using CommunityLibrarySystem.Domain.ValueObjects;

namespace CommunityLibrarySystem.Infrastructure.Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly LibraryDbContext _context;

        public UsuarioRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public Usuario ObterPorEmail(string email)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email.Endereco == email);
            if (usuario == null) return null;

            return new Usuario(
                usuario.Nome,
                usuario.Email,
                Senha.Reidratar(usuario.SenhaHash.Hash)
            );
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
    }
}
