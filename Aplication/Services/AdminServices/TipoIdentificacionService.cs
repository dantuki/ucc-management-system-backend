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
            data.IdTipoIdentificacion = 0;
            data.Habilitado = true;

            await _repository.AddAsync(data);
            await _unitOfWork.CommitAsync();
            return new ResponseBase<int>(HttpStatusCode.Created, "Registro creado con éxito", data.IdTipoIdentificacion);
        }

        public async Task<ResponseBase<TipoIdentificacionDto>> DeleteAsync(int id)
        {
            var registro = await _repository.GetAsync(x => x.IdTipoIdentificacion == id);
            if (registro == null)
            {
                return new ResponseBase<TipoIdentificacionDto>(HttpStatusCode.NotFound, "No existe el ID " + id, null, 0);
            }

            registro.Habilitado = false;
            await _repository.UpdateAsync(registro);
            await _unitOfWork.CommitAsync();

            var data = _mapper.Map<TipoIdentificacionDto>(registro);
            return new ResponseBase<TipoIdentificacionDto>(HttpStatusCode.OK, "Registro deshabilitado con éxito", data, 1);
        }

        public async Task<ResponseBase<IEnumerable<TipoIdentificacionDto>>> GetAllAsync()
        {
            var lista = await _repository.GetAllAsync();
            var listaActiva = lista.Where(x => x.Habilitado == true).ToList();
            var data = _mapper.Map<IEnumerable<TipoIdentificacionDto>>(listaActiva);
            return new ResponseBase<IEnumerable<TipoIdentificacionDto>>(HttpStatusCode.OK, "OK", data, data.Count());
        }

        public async Task<ResponseBase<TipoIdentificacionDto>> GetByIdAsync(int id)
        {
            var registro = await _repository.GetAsync(x => x.IdTipoIdentificacion == id && x.Habilitado == true);
            if (registro == null)
            {
                return new ResponseBase<TipoIdentificacionDto>(HttpStatusCode.NotFound, "El registro no existe o ha sido eliminado", null, 0);
            }

            var data = _mapper.Map<TipoIdentificacionDto>(registro);
            return new ResponseBase<TipoIdentificacionDto>(HttpStatusCode.OK, "OK", data, 1);
        }

        public async Task<ResponseBase<bool>> UpdateAsync(TipoIdentificacionDto entity)
        {
            var check = await _repository.GetAsync(x => x.IdTipoIdentificacion == entity.IdTipoIdentificacion);
            if (check == null)
            {
                return new ResponseBase<bool>(HttpStatusCode.NotFound, "No existe el ID " + entity.IdTipoIdentificacion, false, 0);
            }

            check.Nombre = entity.Nombre;
            check.Abreviatura = entity.Abreviatura;
            check.Habilitado = entity.Habilitado;

            await _repository.UpdateAsync(check);
            await _unitOfWork.CommitAsync();
            return new ResponseBase<bool>(HttpStatusCode.OK, "Registro actualizado con éxito", true, 1);
        }
    }
}