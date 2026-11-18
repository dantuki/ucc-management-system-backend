using Domain.Models.Admin;
using Domain.Models.AdminModels;
using Domain.Models.GestionModels;
using Microsoft.EntityFrameworkCore;
using Persistence.FluentConfig.AdminConfig;
using Persistence.FluentConfig.GestionConfig;
using Persistence.Repository.IRepositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Rol> Rol { get; set; }
        public DbSet<EstadoCivil> EstadoCivil { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<TipoIdentificacion> TipoIdentificacion { get; set; }
        public DbSet<TipoFormacion> TipoFormacion { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region ADMIN
            // Mapeo automático con el diseño de tu draw.io
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(e => e.IdUsuario);
                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
                entity.Property(e => e.NombreUsuario).HasColumnName("nombre_usuario");
                entity.Property(e => e.Contrasena).HasColumnName("contrasena");
                entity.Property(e => e.Habilitado).HasColumnName("habilitado");
                entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");
                entity.Property(e => e.FechaModificacion).HasColumnName("fecha_modificacion");
                entity.Property(e => e.IdPersona).HasColumnName("id_persona");

                // Relación 1:1 con Persona (La llave física se queda en la tabla Usuario)
                entity.HasOne(u => u.Persona)
                      .WithOne(p => p.Usuario)
                      .HasForeignKey<Usuario>(u => u.IdPersona)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Mapeo de la nueva entidad Persona siguiendo tu estándar de base de datos
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona");
                entity.HasKey(e => e.IdPersona);
                entity.Property(e => e.IdPersona).HasColumnName("id_persona");
                entity.Property(e => e.Nombres).HasColumnName("nombres").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Apellidos).HasColumnName("apellidos").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Documento).HasColumnName("documento").IsRequired().HasMaxLength(20);
                entity.Property(e => e.Correo).HasColumnName("correo").IsRequired().HasMaxLength(150);
                entity.Property(e => e.Telefono).HasColumnName("telefono").HasMaxLength(20);
                entity.Property(e => e.IdGenero).HasColumnName("id_genero");
                entity.Property(e => e.IdEstadoCivil).HasColumnName("id_estado_civil");

                // Relación Persona -> Genero
                entity.HasOne(p => p.Genero)
                      .WithMany()
                      .HasForeignKey(p => p.IdGenero)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación Persona -> EstadoCivil
                entity.HasOne(p => p.EstadoCivil)
                      .WithMany()
                      .HasForeignKey(p => p.IdEstadoCivil)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(p => p.Documento).IsUnique();
                entity.HasIndex(p => p.Correo).IsUnique();
            });

            new RolConfig(modelBuilder.Entity<Rol>());
            new EstadoCivilConfig(modelBuilder.Entity<EstadoCivil>());
            new GeneroConfig(modelBuilder.Entity<Genero>());
            new TipoIdentificacionConfig(modelBuilder.Entity<TipoIdentificacion>());
            new TipoFormacionConfig(modelBuilder.Entity<TipoFormacion>());
            #endregion

            new EstudianteConfig(modelBuilder.Entity<Estudiante>());

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");
                entity.HasKey(permission => permission.Code);
                entity.Property(permission => permission.Code).HasMaxLength(64);
                entity.Property(permission => permission.Description).HasMaxLength(160).IsRequired();
                entity.HasData(Domain.Security.PermissionCodes.All.Select(permission =>
                    new Permission { Code = permission.Key, Description = permission.Value }));
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UsuarioRol");
                entity.HasKey(userRole => new { userRole.IdUsuario, userRole.IdRol });
                entity.HasOne(userRole => userRole.User)
                    .WithMany(user => user.UserRoles)
                    .HasForeignKey(userRole => userRole.IdUsuario)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(userRole => userRole.Role)
                    .WithMany(role => role.UserRoles)
                    .HasForeignKey(userRole => userRole.IdRol)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.ToTable("RolPermiso");
                entity.HasKey(rolePermission => new { rolePermission.IdRol, rolePermission.PermissionCode });
                entity.Property(rolePermission => rolePermission.PermissionCode).HasMaxLength(64);
                entity.HasOne(rolePermission => rolePermission.Role)
                    .WithMany(role => role.RolePermissions)
                    .HasForeignKey(rolePermission => rolePermission.IdRol)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(rolePermission => rolePermission.Permission)
                    .WithMany(permission => permission.RolePermissions)
                    .HasForeignKey(rolePermission => rolePermission.PermissionCode)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public void Commit()
        {
            SaveChanges();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return SaveChangesAsync(cancellationToken);
        }

        public DbContext GetContext()
        {
            return this;
        }

        public DbSet<TEntity> GetSet<TId, TEntity>()
            where TId : struct
            where TEntity : class
        {
            return Set<TEntity>();
        }
    }
}
