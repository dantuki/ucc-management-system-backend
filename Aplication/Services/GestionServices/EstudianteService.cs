using AutoMapper;
using Application.Services.GestionServices;
using Domain.DTOs.GestionDTOs;
using Domain.Models.GestionModels;
using Persistence.Repository.IRepositories.GestionIRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.GestionServices
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudianteRepository _estudianteRepository;
        private readonly IMapper _mapper;

        public EstudianteService(IEstudianteRepository estudianteRepository, IMapper mapper)
        {
            _estudianteRepository = estudianteRepository;
            _mapper = mapper;
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
            var estudiante = _mapper.Map<Estudiante>(request);
            await _estudianteRepository.AddAsync(estudiante);
            return _mapper.Map<EstudianteDto>(estudiante);
        }

        public async Task<bool> UpdateAsync(int id, EstudianteRequestDto request)
        {
            var estudianteExistente = await _estudianteRepository.FindByIdAsync(id);
            if (estudianteExistente == null) return false;

            _mapper.Map(request, estudianteExistente);
            await _estudianteRepository.UpdateAsync(estudianteExistente);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var estudiante = await _estudianteRepository.FindByIdAsync(id);
            if (estudiante == null) return false;

            await _estudianteRepository.DeleteAsync(estudiante);
            return true;
        }
    }
}