using CatalogoApp.Domain.Interfaces;
using System.Text.Json;

namespace CatalogoApp.Infrastructure.Repositories
{
    public class JsonFavoriteRepository : IFavoriteRepository
    {
        private readonly string _rutaArchivo;

        public JsonFavoriteRepository(string rutaArchivo)
        {
            // Si le pasan un directorio o una ruta sin extensión, usar archivo por defecto dentro
            if (Directory.Exists(rutaArchivo) || string.IsNullOrWhiteSpace(Path.GetExtension(rutaArchivo)))
            {
                Directory.CreateDirectory(rutaArchivo);
                rutaArchivo = Path.Combine(rutaArchivo, "favorites.json");
            }

            _rutaArchivo = rutaArchivo;

            var dir = Path.GetDirectoryName(_rutaArchivo);
            if (!string.IsNullOrWhiteSpace(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (!File.Exists(_rutaArchivo))
            {
                File.WriteAllText(_rutaArchivo, "{}");
            }
        }

        public List<int> ObtenerPorUsuario(string usuario)
        {
            var json = File.ReadAllText(_rutaArchivo);

            var data =
                JsonSerializer.Deserialize
                <Dictionary<string, List<int>>>(json)
                ?? new Dictionary<string, List<int>>();

            if (data.ContainsKey(usuario))
            {
                return data[usuario];
            }

            return new List<int>();
        }

        public void Guardar(
            string usuario,
            List<int> favoritos
        )
        {
            var json = File.ReadAllText(_rutaArchivo);

            var data =
                JsonSerializer.Deserialize
                <Dictionary<string, List<int>>>(json)
                ?? new Dictionary<string, List<int>>();

            data[usuario] = favoritos;

            var nuevoJson =
                JsonSerializer.Serialize(
                    data,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

            File.WriteAllText(
                _rutaArchivo,
                nuevoJson
            );
        }

        // IMPLEMENTACION INTERFAZ

        public List<int> ObtenerFavoritos(string usuario)
        {
            return ObtenerPorUsuario(usuario);
        }

        public void GuardarFavoritos(
            string usuario,
            List<int> favoritos
        )
        {
            Guardar(usuario, favoritos);
        }
    }
}