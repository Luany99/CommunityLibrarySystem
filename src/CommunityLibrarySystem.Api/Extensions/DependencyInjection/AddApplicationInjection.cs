using CommunityLibrarySystem.Application.Interfaces;
using CommunityLibrarySystem.Application.Services;

namespace CommunityLibrarySystem.Api.Extensions.DependencyInjection
{
    public static class AddApplicationInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IEmprestimoService, EmprestimoService>();
            services.AddScoped<IAutenticacaoService, AutenticacaoService>();
            return services;
        }
    }
}
