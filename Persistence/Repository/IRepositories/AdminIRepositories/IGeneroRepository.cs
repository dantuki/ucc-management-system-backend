using Domain.Models.Admin;
using Persistence.Repository.IRepositories.BaseRepository;

namespace Persistence.Repository.IRepositories.AdminIRepositories
{
    public interface IGeneroRepository : IBaseRepository<int, Genero>
    {
    }
}