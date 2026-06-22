using Domain.Models.AdminModels;
using Persistence.Context;
using Persistence.Repository.IRepositories.AdminIRepositories;

namespace Persistence.Repository.Repositories.AdminRepositories
{
    public class PersonaRepository : Persistence.Repository.Repositories.BaseRepository.BaseRepository<int, Persona>, IPersonaRepository
    {
        public PersonaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}