using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.DTOs.BaseDTOs;
using Domain.Models.Admin;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Persistence.Repository.IRepositories.AdminIRepositories;
using Persistence.Repository.IRepositories.Generic;
using Persistence.Repository.Repositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
            data.Habilitado = true;

            await _rolRepository.AddAsync(data);

            await _unitOfWork.CommitAsync();

            return new ResponseBase<int>(HttpStatusCode.Created, "Registro creado", data.IdRol);
        }

        public Task<ResponseBase<RolDto>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseBase<IEnumerable<RolDto>>> GetAllAsync()
        {
            var roles = await _rolRepository.GetAllAsync();
            var data = _mapper.Map<IEnumerable<RolDto>>(roles);
            return new ResponseBase<IEnumerable<RolDto>>(HttpStatusCode.OK, message: "OK", data, data.Count());

        }

        public async Task<ResponseBase<RolDto>> GetByIdAsync(int id)
        {
            var rol = await _rolRepository.GetAsync(x => x.IdRol == id);
            var data = _mapper.Map<RolDto>(rol);
            return new ResponseBase<RolDto>(HttpStatusCode.OK, message: "OK", data, 1);
        }

        public async Task<ResponseBase<bool>> UpdateAsync(RolDto entity)
        {
            var rol = GetByIdAsync(entity.IdRol);

            if (rol == null)
            {
                return new ResponseBase<bool>(HttpStatusCode.InternalServerError, message: "No existe" +
                    " el ROL con ID " + entity.IdRol, false, 0);

            }
            var data = new Rol
            {
                IdRol = entity.IdRol,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion,
                Habilitado = entity.Habilitado
            };

            await _rolRepository.UpdateAsync(data);

            await _unitOfWork.CommitAsync();

            return new ResponseBase<bool>(HttpStatusCode.OK, "Registro actualizado", true, 1);

        }
    }
}
