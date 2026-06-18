using Domain.Models.Admin;
using Domain.Models.AdminModels;
using Microsoft.EntityFrameworkCore;
using Persistence.FluentConfig.AdminConfig;
using Persistence.Repository.IRepositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region ADMIN
            new RolConfig(modelBuilder.Entity<Rol>());
            new EstadoCivilConfig(modelBuilder.Entity<EstadoCivil>());
            new GeneroConfig(modelBuilder.Entity<Genero>());
            new TipoIdentificacionConfig(modelBuilder.Entity<TipoIdentificacion>());
            new TipoFormacionConfig(modelBuilder.Entity<TipoFormacion>());
            #endregion

            #region GESTION
            #endregion
        }

        public void Commit()
        {
            try
            {
                using var transactionScope = new TransactionScope();
                SaveChanges();
                transactionScope.Complete();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                await SaveChangesAsync();
                transactionScope.Complete();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
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