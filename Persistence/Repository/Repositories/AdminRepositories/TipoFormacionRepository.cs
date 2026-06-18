using Domain.Models.Admin;
using Persistence.Context;
using Persistence.Repository.IRepositories.AdminIRepositories;
using Persistence.Repository.Repositories.BaseRepository;

namespace Persistence.Repository.Repositories.AdminRepositories
{
    public class TipoFormacionRepository : BaseRepository<int, TipoFormacion>, ITipoFormacionRepository
    {
        public TipoFormacionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}