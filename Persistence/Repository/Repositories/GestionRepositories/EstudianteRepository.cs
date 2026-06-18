using Domain.Models.GestionModels;
using Persistence.Context;
using Persistence.Repository.IRepositories.GestionIRepositories;
using Persistence.Repository.Repositories.BaseRepository;

namespace Persistence.Repository.Repositories.GestionRepositories
{
    public class EstudianteRepository : BaseRepository<int, Estudiante>, IEstudianteRepository
    {
        public EstudianteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}