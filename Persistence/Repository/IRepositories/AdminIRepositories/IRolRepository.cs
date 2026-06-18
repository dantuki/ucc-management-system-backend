using Domain.Models.Admin;
using Persistence.Repository.IRepositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository.IRepositories.AdminIRepositories
{
    public interface IRolRepository : IBaseRepository<int, Rol>
    {
    }
}
