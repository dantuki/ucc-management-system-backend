using Domain.Models.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.FluentConfig.AdminConfig
{
    public class GeneroConfig
    {
        public GeneroConfig(EntityTypeBuilder<Genero> entityBuilder)
        {
            entityBuilder.ToTable("Genero");
            entityBuilder.HasKey(p => p.IdGenero);
            entityBuilder.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            entityBuilder.Property(p => p.Habilitado).IsRequired();
        }
    }
}