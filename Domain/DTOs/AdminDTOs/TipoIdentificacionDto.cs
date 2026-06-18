using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.AdminDTOs
{
    public class TipoIdentificacionDto
    {
        public int IdTipoIdentificacion { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public bool Habilitado { get; set; }
    }

    public class TipoIdentificacionRequestDto
    {
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
    }
}