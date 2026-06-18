using System;
using Domain.Models.AdminModels;
using Domain.Models.Admin;

namespace Domain.Models.GestionModels
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; } = null!;

        // Relaciones (Llaves Foráneas a las tablas de Admin)
        public int TipoIdentificacionId { get; set; }
        public virtual TipoIdentificacion TipoIdentificacion { get; set; } = null!;

        public int GeneroId { get; set; }
        public virtual Genero Genero { get; set; } = null!;

        public int EstadoCivilId { get; set; }
        public virtual EstadoCivil EstadoCivil { get; set; } = null!;

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
    }
}