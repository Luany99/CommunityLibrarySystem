using CommunityLibrarySystem.Application.Interfaces;
using CommunityLibrarySystem.Domain.Repositories;
using CommunityLibrarySystem.Infrastructure.Authentication;
using CommunityLibrarySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CommunityLibrarySystem.Api.Extensions.DependencyInjection
{
    public static class AddInfrastructureInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
