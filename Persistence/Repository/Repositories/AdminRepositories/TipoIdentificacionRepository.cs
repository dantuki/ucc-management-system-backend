using Domain.Models.Admin;
using Persistence.Context;
using Persistence.Repository.IRepositories.AdminIRepositories;
using Persistence.Repository.Repositories.BaseRepository;

namespace Persistence.Repository.Repositories.AdminRepositories
{
    public class TipoIdentificacionRepository : BaseRepository<int, TipoIdentificacion>, ITipoIdentificacionRepository
    {
        public TipoIdentificacionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}