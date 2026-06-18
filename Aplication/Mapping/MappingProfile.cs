using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.Models.Admin; 
using Domain.Models.AdminModels; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.GestionModels;
using Domain.DTOs.GestionDTOs;
namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeos de Rol
            CreateMap<RolDto, Rol>().ReverseMap();
            CreateMap<RolRequestDto, RolDto>().ReverseMap();

            // Mapeos de Genero
            CreateMap<GeneroDto, Genero>().ReverseMap();
            CreateMap<GeneroRequestDto, GeneroDto>().ReverseMap();

            // Mapeos de TipoIdentificacion
            CreateMap<TipoIdentificacionDto, TipoIdentificacion>().ReverseMap();
            CreateMap<TipoIdentificacionRequestDto, TipoIdentificacionDto>().ReverseMap();

            // Mapeos de TipoFormacion
            CreateMap<TipoFormacionDto, TipoFormacion>().ReverseMap();
            CreateMap<TipoFormacionRequestDto, TipoFormacionDto>().ReverseMap();

            // Mapeos de EstadoCivil
            CreateMap<EstadoCivilDto, EstadoCivil>().ReverseMap();

            // Mapeos de Usuario
            CreateMap<UsuarioDto, Usuario>().ReverseMap();
            // Mapeos de Estudiante
            CreateMap<Estudiante, EstudianteDto>().ReverseMap();
            CreateMap<EstudianteRequestDto, Estudiante>();
        }
    }
}