using CommunityLibrarySystem.Domain.Entities;

namespace CommunityLibrarySystem.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario ObterPorEmail(string email);
        void Adicionar(Usuario usuario);
    }
}
