using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.DTOs.BaseDTOs;
using Domain.Models.Admin;
using Persistence.Repository.IRepositories.AdminIRepositories;
using Persistence.Repository.IRepositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Services.AdminServices
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RolService(IRolRepository rolRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseBase<int>> AddAsync(RolDto entity)
        {
            var data = _mapper.Map<Rol>(entity);
            data.IdRol = 0; // Forzamos el ID en 0 para que la BD autoincremente
            data.Habilitado = true;

            await _rolRepository.AddAsync(data);
            await _unitOfWork.CommitAsync();

            return new ResponseBase<int>(HttpStatusCode.Created, "Registro creado con éxito", data.IdRol);
        }

        public async Task<ResponseBase<RolDto>> DeleteAsync(int id)
        {
            var registro = await _rolRepository.GetAsync(x => x.IdRol == id);
            if (registro == null)
            {
                return new ResponseBase<RolDto>(HttpStatusCode.NotFound, "No existe el ROL con ID " + id, null, 0);
            }

            registro.Habilitado = false;
            await _rolRepository.UpdateAsync(registro);
            await _unitOfWork.CommitAsync();

            var data = _mapper.Map<RolDto>(registro);
            return new ResponseBase<RolDto>(HttpStatusCode.OK, "Registro deshabilitado con éxito", data, 1);
        }

        public async Task<ResponseBase<IEnumerable<RolDto>>> GetAllAsync()
        {
            var roles = await _rolRepository.GetAllAsync();
            var rolesActivos = roles.Where(x => x.Habilitado == true).ToList();
            var data = _mapper.Map<IEnumerable<RolDto>>(rolesActivos);
            return new ResponseBase<IEnumerable<RolDto>>(HttpStatusCode.OK, "OK", data, data.Count());
        }

        public async Task<ResponseBase<RolDto>> GetByIdAsync(int id)
        {
            var rol = await _rolRepository.GetAsync(x => x.IdRol == id && x.Habilitado == true);
            if (rol == null)
            {
                return new ResponseBase<RolDto>(HttpStatusCode.NotFound, "El rol no existe o ha sido deshabilitado", null, 0);
            }

            var data = _mapper.Map<RolDto>(rol);
            return new ResponseBase<RolDto>(HttpStatusCode.OK, "OK", data, 1);
        }

        public async Task<ResponseBase<bool>> UpdateAsync(RolDto entity)
        {
            var check = await _rolRepository.GetAsync(x => x.IdRol == entity.IdRol);
            if (check == null)
            {
                return new ResponseBase<bool>(HttpStatusCode.NotFound, "No existe el ROL con ID " + entity.IdRol, false, 0);
            }

            check.Nombre = entity.Nombre;
            check.Descripcion = entity.Descripcion;
            check.Habilitado = entity.Habilitado;

            await _rolRepository.UpdateAsync(check);
            await _unitOfWork.CommitAsync();

            return new ResponseBase<bool>(HttpStatusCode.OK, "Registro actualizado con éxito", true, 1);
        }
    }
}