using Domain.Models.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.FluentConfig.AdminConfig
{
    public class TipoIdentificacionConfig
    {
        public TipoIdentificacionConfig(EntityTypeBuilder<TipoIdentificacion> entityBuilder)
        {
            entityBuilder.ToTable("TipoIdentificacion");
            entityBuilder.HasKey(p => p.IdTipoIdentificacion);
            entityBuilder.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            entityBuilder.Property(p => p.Abreviatura).IsRequired().HasMaxLength(10);
            entityBuilder.Property(p => p.Habilitado).IsRequired();
        }
    }
}