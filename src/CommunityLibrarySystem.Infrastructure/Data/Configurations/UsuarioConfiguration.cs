using CommunityLibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityLibrarySystem.Infrastructure.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Endereco)
                     .HasColumnName("Email")
                     .IsRequired();
            });

            builder.OwnsOne(u => u.SenhaHash, senha =>
            {
                senha.Property(s => s.Hash)
                     .HasColumnName("SenhaHash")
                     .IsRequired();
            });

            builder.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
