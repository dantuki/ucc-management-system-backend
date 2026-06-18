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
    public class TipoFormacionService : ITipoFormacionService
    {
        private readonly ITipoFormacionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TipoFormacionService(ITipoFormacionRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseBase<int>> AddAsync(TipoFormacionDto entity)
        {
            var data = _mapper.Map<TipoFormacion>(entity);
            data.IdTipoFormacion = 0;
            data.Habilitado = true;

            await _repository.AddAsync(data);
            await _unitOfWork.CommitAsync();
            return new ResponseBase<int>(HttpStatusCode.Created, "Registro creado con éxito", data.IdTipoFormacion);
        }

        public async Task<ResponseBase<TipoFormacionDto>> DeleteAsync(int id)
        {
            var registro = await _repository.GetAsync(x => x.IdTipoFormacion == id);
            if (registro == null)
            {
                return new ResponseBase<TipoFormacionDto>(HttpStatusCode.NotFound, "No existe el ID " + id, null, 0);
            }

            registro.Habilitado = false;
            await _repository.UpdateAsync(registro);
            await _unitOfWork.CommitAsync();

            var data = _mapper.Map<TipoFormacionDto>(registro);
            return new ResponseBase<TipoFormacionDto>(HttpStatusCode.OK, "Registro deshabilitado con éxito", data, 1);
        }

        public async Task<ResponseBase<IEnumerable<TipoFormacionDto>>> GetAllAsync()
        {
            var lista = await _repository.GetAllAsync();
            var listaActiva = lista.Where(x => x.Habilitado == true).ToList();
            var data = _mapper.Map<IEnumerable<TipoFormacionDto>>(listaActiva);
            return new ResponseBase<IEnumerable<TipoFormacionDto>>(HttpStatusCode.OK, "OK", data, data.Count());
        }

        public async Task<ResponseBase<TipoFormacionDto>> GetByIdAsync(int id)
        {
            var registro = await _repository.GetAsync(x => x.IdTipoFormacion == id && x.Habilitado == true);
            if (registro == null)
            {
                return new ResponseBase<TipoFormacionDto>(HttpStatusCode.NotFound, "El registro no existe o ha sido eliminado", null, 0);
            }

            var data = _mapper.Map<TipoFormacionDto>(registro);
            return new ResponseBase<TipoFormacionDto>(HttpStatusCode.OK, "OK", data, 1);
        }

        public async Task<ResponseBase<bool>> UpdateAsync(TipoFormacionDto entity)
        {
            var check = await _repository.GetAsync(x => x.IdTipoFormacion == entity.IdTipoFormacion);
            if (check == null)
            {
                return new ResponseBase<bool>(HttpStatusCode.NotFound, "No existe el ID " + entity.IdTipoFormacion, false, 0);
            }

            check.Nombre = entity.Nombre;
            check.Habilitado = entity.Habilitado;

            await _repository.UpdateAsync(check);
            await _unitOfWork.CommitAsync();
            return new ResponseBase<bool>(HttpStatusCode.OK, "Registro actualizado con éxito", true, 1);
        }
    }
}