using Domain.Models.Admin;
using Persistence.Repository.IRepositories.BaseRepository;

namespace Persistence.Repository.IRepositories.AdminIRepositories
{
    public interface ITipoFormacionRepository : IBaseRepository<int, TipoFormacion>
    {
    }
}