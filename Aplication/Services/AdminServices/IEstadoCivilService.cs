using Domain.DTOs.AdminDTOs;
using Domain.DTOs.BaseDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.AdminServices
{
    public interface IEstadoCivilService
    {
        Task<ResponseBase<int>> AddAsync(EstadoCivilDto entity);
        Task<ResponseBase<EstadoCivilDto>> DeleteAsync(int id);
        Task<ResponseBase<IEnumerable<EstadoCivilDto>>> GetAllAsync();
        Task<ResponseBase<EstadoCivilDto>> GetByIdAsync(int id);
        Task<ResponseBase<bool>> UpdateAsync(EstadoCivilDto entity);
    }
}