using CommunityLibrarySystem.Domain.Entities;

namespace CommunityLibrarySystem.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GerarToken(Usuario usuario);
    }
}
