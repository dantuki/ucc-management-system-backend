namespace Domain.DTOs.AdminDTOs
{
    public class EstadoCivilDto
    {
        public int IdEstadoCivil { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Habilitado { get; set; }
    }
}