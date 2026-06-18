using Domain.Models.AdminModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.FluentConfig.AdminConfig
{
    public class EstadoCivilConfig
    {
        public EstadoCivilConfig(EntityTypeBuilder<EstadoCivil> entity)
        {
            entity.ToTable("EstadoCivil");

            entity.HasKey(x => x.IdEstadoCivil);

            entity.Property(x => x.Nombre).IsRequired().HasMaxLength(50);
            entity.Property(x => x.Descripcion).HasMaxLength(150);
            entity.Property(x => x.Habilitado).IsRequired();
        }
    }
}
