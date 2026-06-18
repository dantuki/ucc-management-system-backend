using System;

namespace Domain.DTOs.GestionDTOs
{
    public class EstudianteRequestDto
    {
        public string Codigo { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; } = null!;
        public int TipoIdentificacionId { get; set; }
        public int GeneroId { get; set; }
        public int EstadoCivilId { get; set; }
        public int UsuarioId { get; set; }
    }
}