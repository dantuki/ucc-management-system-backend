using Domain.DTOs.AdminDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.AdminServices
{
    public interface IEstadoCivilService
    {
        Task<IEnumerable<EstadoCivilDto>> GetAllAsync();
        Task<EstadoCivilDto> GetByIdAsync(int id);
        Task AddAsync(EstadoCivilDto dto);
        Task UpdateAsync(EstadoCivilDto dto);
        Task DeleteAsync(int id);
    }
}