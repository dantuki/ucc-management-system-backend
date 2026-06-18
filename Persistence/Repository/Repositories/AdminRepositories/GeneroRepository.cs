using Domain.Models.Admin;
using Persistence.Context;
using Persistence.Repository.IRepositories.AdminIRepositories;
using Persistence.Repository.Repositories.BaseRepository;

namespace Persistence.Repository.Repositories.AdminRepositories
{
    public class GeneroRepository : BaseRepository<int, Genero>, IGeneroRepository
    {
        public GeneroRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}