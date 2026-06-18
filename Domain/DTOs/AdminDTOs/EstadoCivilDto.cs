namespace Domain.DTOs.AdminDTOs
{
    public class EstadoCivilDto
    {
        public int IdEstadoCivil { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Habilitado { get; set; }
    }
}