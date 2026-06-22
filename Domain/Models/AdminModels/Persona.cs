using Domain.Models.Admin;

namespace Domain.Models.AdminModels
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        // Relaciones con las tablas maestras
        public int IdGenero { get; set; }
        public Genero? Genero { get; set; }

        public int IdEstadoCivil { get; set; }
        public EstadoCivil? EstadoCivil { get; set; }

        // Relación inversa 1:1 con Usuario
        public Usuario? Usuario { get; set; }
    }
}