using Domain.Models.GestionModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.FluentConfig.GestionConfig;

public sealed class EstudianteConfig
{
    public EstudianteConfig(EntityTypeBuilder<Estudiante> entity)
    {
        entity.ToTable("Estudiante");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("id_estudiante");
        entity.Property(x => x.Codigo).HasColumnName("codigo").IsRequired().HasMaxLength(30);
        entity.Property(x => x.Nombres).HasColumnName("nombres").IsRequired().HasMaxLength(100);
        entity.Property(x => x.Apellidos).HasColumnName("apellidos").IsRequired().HasMaxLength(100);
        entity.Property(x => x.Documento).HasColumnName("documento").IsRequired().HasMaxLength(20);
        entity.Property(x => x.FechaNacimiento).HasColumnName("fecha_nacimiento");
        entity.Property(x => x.Correo).HasColumnName("correo").IsRequired().HasMaxLength(150);
        entity.Property(x => x.TipoIdentificacionId).HasColumnName("id_tipo_identificacion");
        entity.Property(x => x.GeneroId).HasColumnName("id_genero");
        entity.Property(x => x.EstadoCivilId).HasColumnName("id_estado_civil");
        entity.Property(x => x.UsuarioId).HasColumnName("id_usuario");

        entity.HasIndex(x => x.Codigo).IsUnique();
        entity.HasIndex(x => x.Documento).IsUnique();
        entity.HasIndex(x => x.UsuarioId).IsUnique();
        entity.HasOne(x => x.TipoIdentificacion).WithMany().HasForeignKey(x => x.TipoIdentificacionId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.Genero).WithMany().HasForeignKey(x => x.GeneroId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.EstadoCivil).WithMany().HasForeignKey(x => x.EstadoCivilId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.Usuario).WithOne().HasForeignKey<Estudiante>(x => x.UsuarioId).OnDelete(DeleteBehavior.Restrict);
    }
}
