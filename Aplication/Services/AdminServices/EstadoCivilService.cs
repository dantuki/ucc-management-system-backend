using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.DTOs.BaseDTOs;
using Domain.Models.AdminModels;
using Persistence.Repository.IRepositories.AdminIRepositories;
using Persistence.Repository.IRepositories.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Services.AdminServices
{
    public class EstadoCivilService : IEstadoCivilService
    {
        private readonly IEstadoCivilRepository _estadoCivilRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EstadoCivilService(IEstadoCivilRepository estadoCivilRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _estadoCivilRepository = estadoCivilRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseBase<int>> AddAsync(EstadoCivilDto entity)
        {
            var data = _mapper.Map<EstadoCivil>(entity);
            data.IdEstadoCivil = 0; 
            data.Habilitado = true;

            await _estadoCivilRepository.AddAsync(data);
            await _unitOfWork.CommitAsync();
            return new ResponseBase<int>(HttpStatusCode.Created, "Registro creado con éxito", data.IdEstadoCivil);
        }

        public async Task<ResponseBase<EstadoCivilDto>> DeleteAsync(int id)
        {
            var registro = await _estadoCivilRepository.GetAsync(x => x.IdEstadoCivil == id);
            if (registro == null)
            {
                return new ResponseBase<EstadoCivilDto>(HttpStatusCode.NotFound, "No existe el ID " + id, null, 0);
            }

            registro.Habilitado = false;
            await _estadoCivilRepository.UpdateAsync(registro);
            await _unitOfWork.CommitAsync();

            var data = _mapper.Map<EstadoCivilDto>(registro);
            return new ResponseBase<EstadoCivilDto>(HttpStatusCode.OK, "Registro deshabilitado con éxito", data, 1);
        }

        public async Task<ResponseBase<IEnumerable<EstadoCivilDto>>> GetAllAsync()
        {
            var lista = await _estadoCivilRepository.GetAllAsync();
            var listaActiva = lista.Where(x => x.Habilitado == true).ToList();
            var data = _mapper.Map<IEnumerable<EstadoCivilDto>>(listaActiva);
            return new ResponseBase<IEnumerable<EstadoCivilDto>>(HttpStatusCode.OK, "OK", data, data.Count());
        }

        public async Task<ResponseBase<EstadoCivilDto>> GetByIdAsync(int id)
        {
            var registro = await _estadoCivilRepository.GetAsync(x => x.IdEstadoCivil == id && x.Habilitado == true);
            if (registro == null)
            {
                return new ResponseBase<EstadoCivilDto>(HttpStatusCode.NotFound, "El registro no existe o ha sido eliminado", null, 0);
            }

            var data = _mapper.Map<EstadoCivilDto>(registro);
            return new ResponseBase<EstadoCivilDto>(HttpStatusCode.OK, "OK", data, 1);
        }

        public async Task<ResponseBase<bool>> UpdateAsync(EstadoCivilDto entity)
        {
            var check = await _estadoCivilRepository.GetAsync(x => x.IdEstadoCivil == entity.IdEstadoCivil);
            if (check == null)
            {
                return new ResponseBase<bool>(HttpStatusCode.NotFound, "No existe el ID " + entity.IdEstadoCivil, false, 0);
            }
            
            check.Nombre = entity.Nombre;
            check.Habilitado = entity.Habilitado;
            
            await _estadoCivilRepository.UpdateAsync(check);
            await _unitOfWork.CommitAsync();
            return new ResponseBase<bool>(HttpStatusCode.OK, "Registro actualizado con éxito", true, 1);
        }
    }
}