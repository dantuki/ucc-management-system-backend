using System;

namespace Domain.DTOs.AdminDTOs
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public bool Habilitado { get; set; }
    }
}