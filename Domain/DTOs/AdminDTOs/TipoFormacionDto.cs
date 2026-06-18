using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.AdminDTOs
{
    public class TipoFormacionDto
    {
        public int IdTipoFormacion { get; set; }
        public string Nombre { get; set; }
        public bool Habilitado { get; set; }
    }

    public class TipoFormacionRequestDto
    {
        public string Nombre { get; set; }
    }
}