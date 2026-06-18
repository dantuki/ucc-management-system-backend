using Domain.Models.GestionModels;
using Persistence.Repository.IRepositories.BaseRepository;

namespace Persistence.Repository.IRepositories.GestionIRepositories
{
    // Cambia 'int' por 'Guid' si el Id de tu estudiante no es entero
    public interface IEstudianteRepository : IBaseRepository<int, Estudiante>
    {
        // Métodos específicos si los hay
    }
}