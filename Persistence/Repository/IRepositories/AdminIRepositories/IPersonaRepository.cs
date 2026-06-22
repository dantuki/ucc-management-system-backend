using Domain.Models.AdminModels;
using Persistence.Repository.IRepositories.BaseRepository;

namespace Persistence.Repository.IRepositories.AdminIRepositories
{
    public interface IPersonaRepository : IBaseRepository<int, Persona>
    {
    }
}