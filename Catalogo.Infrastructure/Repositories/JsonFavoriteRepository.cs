using System.Text.Json;

namespace CatalogoApp.Infrastructure.Repositories
{
    public class JsonFavoriteRepository
    {
        private readonly string _dataPath;

        public JsonFavoriteRepository(string dataPath)
        {
            _dataPath = dataPath;
        }

        public List<int> ObtenerPorUsuario(string usuario)
        {
            var filePath = ObtenerRuta(usuario);

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }

            var json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<List<int>>(json)
                ?? new List<int>();
        }

        public void Guardar(string usuario, List<int> favoritos)
        {
            var filePath = ObtenerRuta(usuario);

            var json = JsonSerializer.Serialize(
                favoritos.Distinct().OrderBy(x => x).ToList(),
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(filePath, json);
        }

        private string ObtenerRuta(string usuario)
        {
            Directory.CreateDirectory(_dataPath);

            var nombreSeguro = new string(usuario
                .Where(x => char.IsLetterOrDigit(x) || x == '-' || x == '_')
                .ToArray());

            if (string.IsNullOrWhiteSpace(nombreSeguro))
            {
                nombreSeguro = "usuario";
            }

            return Path.Combine(
                _dataPath,
                $"favoritos_{nombreSeguro.ToLowerInvariant()}.json");
        }
    }
}
