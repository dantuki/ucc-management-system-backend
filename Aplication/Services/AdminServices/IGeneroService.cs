using Domain.DTOs.AdminDTOs;
using Domain.DTOs.BaseDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.AdminServices
{
    public interface IGeneroService
    {
        Task<ResponseBase<IEnumerable<GeneroDto>>> GetAllAsync();
        Task<ResponseBase<GeneroDto>> GetByIdAsync(int id);
        Task<ResponseBase<int>> AddAsync(GeneroDto entity);
        Task<ResponseBase<bool>> UpdateAsync(GeneroDto entity);
        // Esta es la firma que coincide con tu código original de GeneroService
        Task<ResponseBase<GeneroDto>> DeleteAsync(int id); 
    }
}