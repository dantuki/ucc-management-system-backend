using Domain.Models.Admin;
using Persistence.Repository.IRepositories.BaseRepository;

namespace Persistence.Repository.IRepositories.AdminIRepositories
{
    public interface ITipoIdentificacionRepository : IBaseRepository<int, TipoIdentificacion>
    {
    }
}