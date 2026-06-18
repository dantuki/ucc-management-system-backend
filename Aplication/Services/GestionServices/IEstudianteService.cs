using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTOs.GestionDTOs;

namespace Application.Services.GestionServices
{
    public interface IEstudianteService
    {
        Task<IEnumerable<EstudianteDto>> GetAllAsync();
        Task<EstudianteDto?> GetByIdAsync(int id);
        Task<EstudianteDto> CreateAsync(EstudianteRequestDto request);
        Task<bool> UpdateAsync(int id, EstudianteRequestDto request);
        Task<bool> DeleteAsync(int id);
    }
}