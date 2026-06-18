using Domain.Models.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.FluentConfig.AdminConfig
{
    public class TipoFormacionConfig
    {
        public TipoFormacionConfig(EntityTypeBuilder<TipoFormacion> entityBuilder)
        {
            entityBuilder.ToTable("TipoFormacion");
            entityBuilder.HasKey(p => p.IdTipoFormacion);
            entityBuilder.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            entityBuilder.Property(p => p.Habilitado).IsRequired();
        }
    }
}