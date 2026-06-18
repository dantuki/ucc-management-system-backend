using Domain.Models.AdminModels;
using Persistence.Context;
using Persistence.Repository.IRepositories.AdminIRepositories;
using Persistence.Repository.Repositories.BaseRepository;

namespace Persistence.Repository.Repositories.AdminRepositories
{
    public class EstadoCivilRepository : BaseRepository<int, EstadoCivil>, IEstadoCivilRepository
    {
        public EstadoCivilRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}