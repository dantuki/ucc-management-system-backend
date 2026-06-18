using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.Models.AdminModels;
using Persistence.Repository.IRepositories.AdminIRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.AdminServices
{
    public class EstadoCivilService : IEstadoCivilService
    {
        private readonly IEstadoCivilRepository _repository;
        private readonly IMapper _mapper;

        public EstadoCivilService(IEstadoCivilRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EstadoCivilDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<EstadoCivilDto>>(entities);
        }

        public async Task<EstadoCivilDto> GetByIdAsync(int id)
        {
            // Corregido con el campo exacto de tu modelo: IdEstadoCivil
            var entity = await _repository.GetAsync(x => x.IdEstadoCivil == id); 
            return _mapper.Map<EstadoCivilDto>(entity);
        }

        public async Task AddAsync(EstadoCivilDto dto)
        {
            var entity = _mapper.Map<EstadoCivil>(dto);
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(EstadoCivilDto dto)
        {
            var entity = _mapper.Map<EstadoCivil>(dto);
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}