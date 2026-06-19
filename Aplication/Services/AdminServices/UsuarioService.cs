using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.DTOs.BaseDTOs;
using Domain.Models.AdminModels;
using Persistence.Repository.IRepositories.AdminIRepositories;
using Persistence.Repository.IRepositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Services.AdminServices
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseBase<int>> AddAsync(UsuarioDto dto)
        {
            var entity = _mapper.Map<Usuario>(dto);
            entity.IdUsuario = 0; // Forzamos autoincremento en base de datos
            entity.Habilitado = true;
            entity.FechaCreacion = DateTime.Now;

            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();

            return new ResponseBase<int>(HttpStatusCode.Created, "Usuario creado con éxito", entity.IdUsuario);
        }

        public async Task<ResponseBase<UsuarioDto>> DeleteAsync(int id)
        {
            var registro = await _repository.GetAsync(x => x.IdUsuario == id);
            if (registro == null)
            {
                return new ResponseBase<UsuarioDto>(HttpStatusCode.NotFound, "No existe el usuario con ID " + id, null, 0);
            }

            registro.Habilitado = false;
            registro.FechaModificacion = DateTime.Now;

            await _repository.UpdateAsync(registro);
            await _unitOfWork.CommitAsync();

            var data = _mapper.Map<UsuarioDto>(registro);
            return new ResponseBase<UsuarioDto>(HttpStatusCode.OK, "Usuario deshabilitado con éxito", data, 1);
        }

        public async Task<ResponseBase<IEnumerable<UsuarioDto>>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var activos = entities.Where(x => x.Habilitado == true).ToList();
            var data = _mapper.Map<IEnumerable<UsuarioDto>>(activos);

            return new ResponseBase<IEnumerable<UsuarioDto>>(HttpStatusCode.OK, "OK", data, data.Count());
        }

        public async Task<ResponseBase<UsuarioDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetAsync(x => x.IdUsuario == id && x.Habilitado == true);
            if (entity == null)
            {
                return new ResponseBase<UsuarioDto>(HttpStatusCode.NotFound, "El usuario no existe o ha sido deshabilitado", null, 0);
            }

            var data = _mapper.Map<UsuarioDto>(entity);
            return new ResponseBase<UsuarioDto>(HttpStatusCode.OK, "OK", data, 1);
        }

        public async Task<ResponseBase<bool>> UpdateAsync(UsuarioDto dto)
        {
            var check = await _repository.GetAsync(x => x.IdUsuario == dto.IdUsuario);
            if (check == null)
            {
                return new ResponseBase<bool>(HttpStatusCode.NotFound, "No existe el usuario con ID " + dto.IdUsuario, false, 0);
            }

            check.NombreUsuario = dto.NombreUsuario;
            check.Contrasena = dto.Contrasena; // Asignación directa por ahora (luego manejamos hashing)
            check.Habilitado = dto.Habilitado;
            check.FechaModificacion = DateTime.Now;

            await _repository.UpdateAsync(check);
            await _unitOfWork.CommitAsync();

            return new ResponseBase<bool>(HttpStatusCode.OK, "Usuario actualizado con éxito", true, 1);
        }
    }
}