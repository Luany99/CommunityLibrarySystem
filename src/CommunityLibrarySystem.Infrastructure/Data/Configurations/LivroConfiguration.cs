using CommunityLibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityLibrarySystem.Infrastructure.Data.Configurations
{
    public class LivroConfiguration : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.Property(x => x.Titulo)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(x => x.Autor)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
