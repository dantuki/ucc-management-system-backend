using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.DTOs.BaseDTOs;
using Domain.Models.Admin;
using Persistence.Repository.IRepositories.AdminIRepositories;
using Persistence.Repository.IRepositories.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Services.AdminServices
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GeneroService(IGeneroRepository generoRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _generoRepository = generoRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // 1. POST - CREAR REGISTRO
        public async Task<ResponseBase<int>> AddAsync(GeneroDto entity)
        {
            var data = _mapper.Map<Genero>(entity);
            
            // Forzamos el ID a 0 para que MySQL use el AUTO_INCREMENT de forma segura
            data.IdGenero = 0; 
            data.Habilitado = true;

            await _generoRepository.AddAsync(data);
            await _unitOfWork.CommitAsync();
            return new ResponseBase<int>(HttpStatusCode.Created, "Registro creado con éxito", data.IdGenero);
        }

        // 2. DELETE - BORRADO LÓGICO
        public async Task<ResponseBase<GeneroDto>> DeleteAsync(int id)
        {
            var registro = await _generoRepository.GetAsync(x => x.IdGenero == id);
            if (registro == null)
            {
                return new ResponseBase<GeneroDto>(HttpStatusCode.NotFound, "No existe el ID " + id, null, 0);
            }

            registro.Habilitado = false;
            await _generoRepository.UpdateAsync(registro);
            await _unitOfWork.CommitAsync();

            var data = _mapper.Map<GeneroDto>(registro);
            return new ResponseBase<GeneroDto>(HttpStatusCode.OK, "Registro deshabilitado con éxito", data, 1);
        }

        // 3. GET ALL - OBTENER ACTIVOS
        public async Task<ResponseBase<IEnumerable<GeneroDto>>> GetAllAsync()
        {
            var lista = await _generoRepository.GetAllAsync();
            
            // Filtramos para que Swagger y el Front solo vean los registros activos
            var listaActiva = lista.Where(x => x.Habilitado == true).ToList();
            
            var data = _mapper.Map<IEnumerable<GeneroDto>>(listaActiva);
            return new ResponseBase<IEnumerable<GeneroDto>>(HttpStatusCode.OK, "OK", data, data.Count());
        }

        // 4. GET BY ID - CONSULTAR DETALLE
        public async Task<ResponseBase<GeneroDto>> GetByIdAsync(int id)
        {
            // Solo lo devuelve si existe y si está habilitado
            var registro = await _generoRepository.GetAsync(x => x.IdGenero == id && x.Habilitado == true);
            if (registro == null)
            {
                return new ResponseBase<GeneroDto>(HttpStatusCode.NotFound, "El registro no existe o ha sido eliminado", null, 0);
            }

            var data = _mapper.Map<GeneroDto>(registro);
            return new ResponseBase<GeneroDto>(HttpStatusCode.OK, "OK", data, 1);
        }

        // 5. PUT - ACTUALIZAR REGISTRO
        public async Task<ResponseBase<bool>> UpdateAsync(GeneroDto entity)
        {
            var check = await _generoRepository.GetAsync(x => x.IdGenero == entity.IdGenero);
            if (check == null)
            {
                return new ResponseBase<bool>(HttpStatusCode.NotFound, "No existe el ID " + entity.IdGenero, false, 0);
            }
            
            // Modificamos el objeto directamente detectado por Entity Framework para evitar errores de llaves o tracking
            check.Nombre = entity.Nombre;
            check.Habilitado = entity.Habilitado;
            
            await _generoRepository.UpdateAsync(check);
            await _unitOfWork.CommitAsync();
            return new ResponseBase<bool>(HttpStatusCode.OK, "Registro actualizado con éxito", true, 1);
        }
    }
}