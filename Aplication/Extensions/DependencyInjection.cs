using Application.Mapping;
using Application.Services.AdminServices;
using Application.Services.Generic;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repository.IRepositories.AdminIRepositories;
using Persistence.Repository.IRepositories.BaseRepository;
using Persistence.Repository.IRepositories.Generic;
using Persistence.Repository.Repositories.AdminRepositories;
using Persistence.Repository.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInterfacesInjection(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());

            #region SERVICES
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IGeneroService, GeneroService>();
            services.AddScoped<ITipoIdentificacionService, TipoIdentificacionService>();
            services.AddScoped<ITipoFormacionService, TipoFormacionService>();
            services.AddScoped<IEstadoCivilService, EstadoCivilService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            #endregion

            #region REPOSITORIES
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IGeneroRepository, GeneroRepository>();
            services.AddScoped<ITipoIdentificacionRepository, TipoIdentificacionRepository>();
            services.AddScoped<ITipoFormacionRepository, TipoFormacionRepository>();
            services.AddScoped<IEstadoCivilRepository, EstadoCivilRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            #endregion

            return services;
        }

        public static IServiceCollection AddServiceDependency(this IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(configurationExpression =>
            {
                configurationExpression.AddProfile(new MappingProfile());
            });
            services.AddSingleton(prop => mapperConfiguration.CreateMapper());
            return services;
        }
    }
}