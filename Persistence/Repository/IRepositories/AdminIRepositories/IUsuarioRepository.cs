using Domain.Models.AdminModels;
using Persistence.Repository.IRepositories.BaseRepository;

namespace Persistence.Repository.IRepositories.AdminIRepositories
{
    public interface IUsuarioRepository : IBaseRepository<int, Usuario>
    {
    }
}