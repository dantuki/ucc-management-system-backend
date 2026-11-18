using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.AdminDtos
{
    public class PersonaDto
    {
        public int IdPersona { get; set; }
        [Required, StringLength(100)] public string Nombres { get; set; } = string.Empty;
        [Required, StringLength(100)] public string Apellidos { get; set; } = string.Empty;
        [Required, StringLength(20)] public string Documento { get; set; } = string.Empty;
        [Required, EmailAddress, StringLength(150)] public string Correo { get; set; } = string.Empty;
        [StringLength(20)] public string Telefono { get; set; } = string.Empty;
        [Range(1, int.MaxValue)] public int IdGenero { get; set; }
        [Range(1, int.MaxValue)] public int IdEstadoCivil { get; set; }
    }
}
