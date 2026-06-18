using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.Models.AdminModels;
using Persistence.Repository.IRepositories.AdminIRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.AdminServices
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UsuarioDto>>(entities);
        }

        public async Task<UsuarioDto> GetByIdAsync(int id)
        {
            // Filtramos usando la PK exacta de tu modelo de Usuario
            var entity = await _repository.GetAsync(x => x.IdUsuario == id);
            return _mapper.Map<UsuarioDto>(entity);
        }

        public async Task AddAsync(UsuarioDto dto)
        {
            var entity = _mapper.Map<Usuario>(dto);
            entity.FechaCreacion = DateTime.Now; // Se asigna automáticamente al crear
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(UsuarioDto dto)
        {
            var entity = _mapper.Map<Usuario>(dto);
            entity.FechaModificacion = DateTime.Now; // Se asigna automáticamente al editar
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}