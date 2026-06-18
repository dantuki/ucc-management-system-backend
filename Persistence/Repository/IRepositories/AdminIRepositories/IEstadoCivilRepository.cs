using Domain.Models.AdminModels;
using Persistence.Repository.IRepositories.BaseRepository;

namespace Persistence.Repository.IRepositories.AdminIRepositories
{
    public interface IEstadoCivilRepository : IBaseRepository<int, EstadoCivil>
    {
    }
}