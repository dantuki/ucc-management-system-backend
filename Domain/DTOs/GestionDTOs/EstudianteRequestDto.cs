using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.GestionDTOs
{
    public class EstudianteRequestDto
    {
        [Required, StringLength(30)] public string Codigo { get; set; } = null!;
        [Required, StringLength(100)] public string Nombres { get; set; } = null!;
        [Required, StringLength(100)] public string Apellidos { get; set; } = null!;
        [Required, StringLength(20)] public string Documento { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        [Required, EmailAddress, StringLength(150)] public string Correo { get; set; } = null!;
        [Range(1, int.MaxValue)] public int TipoIdentificacionId { get; set; }
        [Range(1, int.MaxValue)] public int GeneroId { get; set; }
        [Range(1, int.MaxValue)] public int EstadoCivilId { get; set; }
        [Range(1, int.MaxValue)] public int UsuarioId { get; set; }
    }
}
