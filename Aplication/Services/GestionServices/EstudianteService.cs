using AutoMapper;
using Application.Services.GestionServices;
using Domain.DTOs.GestionDTOs;
using Domain.Models.GestionModels;
using Persistence.Repository.IRepositories.GestionIRepositories;
using Persistence.Repository.IRepositories.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.GestionServices
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EstudianteService(IEstudianteRepository estudianteRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _estudianteRepository = estudianteRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EstudianteDto>> GetAllAsync()
        {
            var estudiantes = await _estudianteRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EstudianteDto>>(estudiantes);
        }

        public async Task<EstudianteDto?> GetByIdAsync(int id)
        {
            var estudiante = await _estudianteRepository.FindByIdAsync(id);
            return estudiante == null ? null : _mapper.Map<EstudianteDto>(estudiante);
        }

        public async Task<EstudianteDto> CreateAsync(EstudianteRequestDto request)
        {
            request.Codigo = request.Codigo.Trim();
            request.Documento = request.Documento.Trim();
            request.Correo = request.Correo.Trim().ToLowerInvariant();
            if (await _estudianteRepository.ExistsAsync(x => x.Codigo == request.Codigo || x.Documento == request.Documento || x.UsuarioId == request.UsuarioId))
            {
                throw new InvalidOperationException("El código, documento o usuario ya está asociado a un estudiante.");
            }

            var estudiante = _mapper.Map<Estudiante>(request);
            await _estudianteRepository.AddAsync(estudiante);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<EstudianteDto>(estudiante);
        }

        public async Task<bool> UpdateAsync(int id, EstudianteRequestDto request)
        {
            var estudianteExistente = await _estudianteRepository.FindByIdAsync(id);
            if (estudianteExistente == null) return false;

            request.Codigo = request.Codigo.Trim();
            request.Documento = request.Documento.Trim();
            request.Correo = request.Correo.Trim().ToLowerInvariant();
            if (await _estudianteRepository.ExistsAsync(x => x.Id != id && (x.Codigo == request.Codigo || x.Documento == request.Documento || x.UsuarioId == request.UsuarioId)))
            {
                throw new InvalidOperationException("El código, documento o usuario ya está asociado a un estudiante.");
            }

            _mapper.Map(request, estudianteExistente);
            await _estudianteRepository.UpdateAsync(estudianteExistente);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var estudiante = await _estudianteRepository.FindByIdAsync(id);
            if (estudiante == null) return false;

            await _estudianteRepository.DeleteAsync(estudiante);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
