using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Admin
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Habilitado { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; } = [];
        public ICollection<UserRole> UserRoles { get; set; } = [];
    }
}
