using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AdminDTOs
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public bool Habilitado { get; set; }
    }

    public class UsuarioCreateRequestDto
    {
        [Required, StringLength(100, MinimumLength = 3)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required, StringLength(128, MinimumLength = 12)]
        public string Contrasena { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int IdPersona { get; set; }
    }

    public class UsuarioUpdateRequestDto
    {
        [Required, StringLength(100, MinimumLength = 3)]
        public string NombreUsuario { get; set; } = string.Empty;

        [StringLength(128, MinimumLength = 12)]
        public string? Contrasena { get; set; }

        public bool Habilitado { get; set; }
    }

    public class LoginRequestDto
    {
        [Required]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required]
        public string Contrasena { get; set; } = string.Empty;
    }
}
