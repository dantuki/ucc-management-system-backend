using AutoMapper;
using Application.Services.AdminServices;
using Domain.Dtos.AdminDtos;
using Domain.Models.AdminModels;
using Persistence.Repository.IRepositories.AdminIRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.AdminServices
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IMapper _mapper;

        public PersonaService(IPersonaRepository personaRepository, IMapper mapper)
        {
            _personaRepository = personaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonaDto>> GetAllAsync()
        {
            var personas = await _personaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PersonaDto>>(personas);
        }

        public async Task<PersonaDto?> GetByIdAsync(int id)
        {
            var persona = await _personaRepository.FindByIdAsync(id);
            return persona == null ? null : _mapper.Map<PersonaDto>(persona);
        }

        public async Task<PersonaDto> CreateAsync(PersonaDto request)
        {
            var persona = _mapper.Map<Persona>(request);
            await _personaRepository.AddAsync(persona);
            return _mapper.Map<PersonaDto>(persona);
        }

        public async Task<bool> UpdateAsync(int id, PersonaDto request)
        {
            var personaExistente = await _personaRepository.FindByIdAsync(id);
            if (personaExistente == null) return false;

            _mapper.Map(request, personaExistente);
            await _personaRepository.UpdateAsync(personaExistente);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var persona = await _personaRepository.FindByIdAsync(id);
            if (persona == null) return false;

            await _personaRepository.DeleteAsync(persona);
            return true;
        }
    }
}