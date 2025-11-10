using CommunityLibrarySystem.Application.DTOs;

namespace CommunityLibrarySystem.Application.Interfaces
{
    public interface IAutenticacaoService
    {
        string Registrar(RegistrarUsuarioDto dto);
        string Autenticar(string email, string senha);
    }
}
