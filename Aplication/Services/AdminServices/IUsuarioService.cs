using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTOs.AdminDTOs;
using Domain.DTOs.BaseDTOs;

namespace Application.Services.AdminServices
{
    public interface IUsuarioService
    {
        Task<ResponseBase<IEnumerable<UsuarioDto>>> GetAllAsync();
        Task<ResponseBase<UsuarioDto>> GetByIdAsync(int id);
        Task<ResponseBase<int>> AddAsync(UsuarioDto dto);
        Task<ResponseBase<bool>> UpdateAsync(UsuarioDto dto);
        Task<ResponseBase<UsuarioDto>> DeleteAsync(int id);
    }
}