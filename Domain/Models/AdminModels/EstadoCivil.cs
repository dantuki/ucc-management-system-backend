namespace Domain.Models.AdminModels
{
    public class EstadoCivil
    {
        public int IdEstadoCivil { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Habilitado { get; set; }
    }
}