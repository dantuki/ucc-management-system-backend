using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Admin
{
    public class TipoFormacion
    {
        public int IdTipoFormacion { get; set; }
        public string Nombre { get; set; }
        public bool Habilitado { get; set; }
    }
}