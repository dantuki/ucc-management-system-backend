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
            data.Habilitado = true;
            await _repository.AddAsync(data);
            await _unitOfWork.CommitAsync();
            return new ResponseBase<int>(HttpStatusCode.Created, "Registro creado", data.IdTipoFormacion);
        }

        public Task<ResponseBase<TipoFormacionDto>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseBase<IEnumerable<TipoFormacionDto>>> GetAllAsync()
        {
            var lista = await _repository.GetAllAsync();
            var data = _mapper.Map<IEnumerable<TipoFormacionDto>>(lista);
            return new ResponseBase<IEnumerable<TipoFormacionDto>>(HttpStatusCode.OK, "OK", data, data.Count());
        }

        public async Task<ResponseBase<TipoFormacionDto>> GetByIdAsync(int id)
        {
            var registro = await _repository.GetAsync(x => x.IdTipoFormacion == id);
            var data = _mapper.Map<TipoFormacionDto>(registro);
            return new ResponseBase<TipoFormacionDto>(HttpStatusCode.OK, "OK", data, 1);
        }

        public async Task<ResponseBase<bool>> UpdateAsync(TipoFormacionDto entity)
        {
            var check = await _repository.GetAsync(x => x.IdTipoFormacion == entity.IdTipoFormacion);
            if (check == null)
            {
                return new ResponseBase<bool>(HttpStatusCode.InternalServerError, "No existe el ID " + entity.IdTipoFormacion, false, 0);
            }
            var data = new TipoFormacion
            {
                IdTipoFormacion = entity.IdTipoFormacion,
                Nombre = entity.Nombre,
                Habilitado = entity.Habilitado
            };
            await _repository.UpdateAsync(data);
            await _unitOfWork.CommitAsync();
            return new ResponseBase<bool>(HttpStatusCode.OK, "Registro actualizado", true, 1);
        }
    }
}