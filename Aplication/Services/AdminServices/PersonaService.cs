using AutoMapper;
using Application.Services.AdminServices;
using Domain.Dtos.AdminDtos;
using Domain.Models.AdminModels;
using Persistence.Repository.IRepositories.AdminIRepositories;
using Persistence.Repository.IRepositories.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.AdminServices
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PersonaService(IPersonaRepository personaRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _personaRepository = personaRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
            request.Correo = request.Correo.Trim().ToLowerInvariant();
            request.Documento = request.Documento.Trim();
            if (await _personaRepository.ExistsAsync(x => x.Documento == request.Documento || x.Correo == request.Correo))
            {
                throw new InvalidOperationException("Ya existe una persona con el documento o correo indicado.");
            }

            var persona = _mapper.Map<Persona>(request);
            await _personaRepository.AddAsync(persona);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<PersonaDto>(persona);
        }

        public async Task<bool> UpdateAsync(int id, PersonaDto request)
        {
            var personaExistente = await _personaRepository.FindByIdAsync(id);
            if (personaExistente == null) return false;

            request.Correo = request.Correo.Trim().ToLowerInvariant();
            request.Documento = request.Documento.Trim();
            if (await _personaRepository.ExistsAsync(x => x.IdPersona != id && (x.Documento == request.Documento || x.Correo == request.Correo)))
            {
                throw new InvalidOperationException("Ya existe una persona con el documento o correo indicado.");
            }

            _mapper.Map(request, personaExistente);
            await _personaRepository.UpdateAsync(personaExistente);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var persona = await _personaRepository.FindByIdAsync(id);
            if (persona == null) return false;

            await _personaRepository.DeleteAsync(persona);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
