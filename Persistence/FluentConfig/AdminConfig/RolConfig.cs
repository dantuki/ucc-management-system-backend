using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Admin;

namespace Persistence.FluentConfig.AdminConfig
{
    class RolConfig
    {
        public RolConfig(EntityTypeBuilder<Rol> entity)
        {
            entity.ToTable("admin.Rol");

            entity.HasKey(x => x.IdRol);

            entity.Property(x => x.Nombre).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Descripcion).IsRequired().HasMaxLength(100);
            entity.Property(x => x.Habilitado).IsRequired();
        }
    }
}
