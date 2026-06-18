using Domain.DTOs.AdminDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.AdminServices
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto> GetByIdAsync(int id);
        Task AddAsync(UsuarioDto dto);
        Task UpdateAsync(UsuarioDto dto);
        Task DeleteAsync(int id);
    }
}