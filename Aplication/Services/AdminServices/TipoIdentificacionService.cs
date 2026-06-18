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
    public class TipoIdentificacionService : ITipoIdentificacionService
    {
        private readonly ITipoIdentificacionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TipoIdentificacionService(ITipoIdentificacionRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseBase<int>> AddAsync(TipoIdentificacionDto entity)
        {
            var data = _mapper.Map<TipoIdentificacion>(entity);
            data.Habilitado = true;
            await _repository.AddAsync(data);
            await _unitOfWork.CommitAsync();
            return new ResponseBase<int>(HttpStatusCode.Created, "Registro creado", data.IdTipoIdentificacion);
        }

        public Task<ResponseBase<TipoIdentificacionDto>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseBase<IEnumerable<TipoIdentificacionDto>>> GetAllAsync()
        {
            var lista = await _repository.GetAllAsync();
            var data = _mapper.Map<IEnumerable<TipoIdentificacionDto>>(lista);
            return new ResponseBase<IEnumerable<TipoIdentificacionDto>>(HttpStatusCode.OK, "OK", data, data.Count());
        }

        public async Task<ResponseBase<TipoIdentificacionDto>> GetByIdAsync(int id)
        {
            var registro = await _repository.GetAsync(x => x.IdTipoIdentificacion == id);
            var data = _mapper.Map<TipoIdentificacionDto>(registro);
            return new ResponseBase<TipoIdentificacionDto>(HttpStatusCode.OK, "OK", data, 1);
        }

        public async Task<ResponseBase<bool>> UpdateAsync(TipoIdentificacionDto entity)
        {
            var check = await _repository.GetAsync(x => x.IdTipoIdentificacion == entity.IdTipoIdentificacion);
            if (check == null)
            {
                return new ResponseBase<bool>(HttpStatusCode.InternalServerError, "No existe el ID " + entity.IdTipoIdentificacion, false, 0);
            }
            var data = new TipoIdentificacion
            {
                IdTipoIdentificacion = entity.IdTipoIdentificacion,
                Nombre = entity.Nombre,
                Abreviatura = entity.Abreviatura,
                Habilitado = entity.Habilitado
            };
            await _repository.UpdateAsync(data);
            await _unitOfWork.CommitAsync();
            return new ResponseBase<bool>(HttpStatusCode.OK, "Registro actualizado", true, 1);
        }
    }
}