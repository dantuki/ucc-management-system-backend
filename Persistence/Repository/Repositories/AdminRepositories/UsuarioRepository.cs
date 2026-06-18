using Domain.Models.AdminModels;
using Persistence.Context;
using Persistence.Repository.IRepositories.AdminIRepositories;
using Persistence.Repository.Repositories.BaseRepository;

namespace Persistence.Repository.Repositories.AdminRepositories
{
    public class UsuarioRepository : BaseRepository<int, Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}