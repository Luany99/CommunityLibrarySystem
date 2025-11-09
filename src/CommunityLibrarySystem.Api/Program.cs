
using CommunityLibrarySystem.Application.Interfaces;
using CommunityLibrarySystem.Application.Services;
using CommunityLibrarySystem.Domain.Repositories;
using CommunityLibrarySystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CommunityLibrarySystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<LibraryDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ILivroRepository, LivroRepository>();
            builder.Services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
            builder.Services.AddScoped<ILivroService, LivroService>();
            builder.Services.AddScoped<IEmprestimoService, EmprestimoService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
