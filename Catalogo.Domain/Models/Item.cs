namespace CatalogoApp.Domain.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;

        public int Ano { get; set; }

        public string Descripcion { get; set; } = string.Empty;

        public string ImagenUrl { get; set; } = string.Empty;

        // Ficha técnica
        public string Cilindrada { get; set; } = string.Empty;
        public string Potencia { get; set; } = string.Empty;
        public string VelocidadMax { get; set; } = string.Empty;
        public string Peso { get; set; } = string.Empty;
        public string Transmision { get; set; } = string.Empty;
        public string CapacidadTanque { get; set; } = string.Empty;
        public string TipoMotor { get; set; } = string.Empty;
    }
}