namespace CatalogoApp.Domain.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Desarrollador { get; set; } = "S/N";

        public string Genero { get; set; } = "S/N";

        public int? Ano { get; set; }

        public string Descripcion { get; set; } = "S/N";

        public string ImagenUrl { get; set; } = "/images/default-game.jpg";

        public string Plataforma { get; set; } = "S/N";

        public string Duracion { get; set; } = "S/N";

        public string Clasificacion { get; set; } = "S/N";
    }
}