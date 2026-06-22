using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.AdminDtos;

namespace Application.Services.AdminServices
{
    public interface IPersonaService
    {
        Task<IEnumerable<PersonaDto>> GetAllAsync();
        Task<PersonaDto?> GetByIdAsync(int id);
        Task<PersonaDto> CreateAsync(PersonaDto request);
        Task<bool> UpdateAsync(int id, PersonaDto request);
        Task<bool> DeleteAsync(int id);
    }
}