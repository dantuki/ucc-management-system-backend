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
        Task<ResponseBase<int>> AddAsync(UsuarioCreateRequestDto dto);
        Task<ResponseBase<bool>> UpdateAsync(int id, UsuarioUpdateRequestDto dto);
        Task<UsuarioDto?> AuthenticateAsync(string nombreUsuario, string contrasena);
        Task<ResponseBase<UsuarioDto>> DeleteAsync(int id);
    }
}
