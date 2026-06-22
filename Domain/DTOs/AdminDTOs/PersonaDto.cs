namespace Domain.Dtos.AdminDtos
{
    public class PersonaDto
    {
        public int IdPersona { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public int IdGenero { get; set; }
        public int IdEstadoCivil { get; set; }
    }
}