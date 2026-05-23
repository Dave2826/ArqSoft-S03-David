using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;
using System.Text.Json;

namespace CatalogoApp.Infrastructure.Repositories
{
    public class JsonItemRepository : IItemRepository
    {
        private readonly string _filePath =
            "Data/items.json";

        public List<Item> ObtenerTodos()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Item>();
            }

            var json =
                File.ReadAllText(_filePath);

            return JsonSerializer.Deserialize<List<Item>>(json)
                   ?? new List<Item>();
        }

        public Item? ObtenerPorId(int id)
        {
            return ObtenerTodos()
                .FirstOrDefault(x => x.Id == id);
        }

        public void Agregar(Item item)
        {
            var items = ObtenerTodos();

            item.Id =
                items.Count > 0
                ? items.Max(x => x.Id) + 1
                : 1;

            items.Add(item);

            Guardar(items);
        }

        public void Eliminar(int id)
        {
            var items = ObtenerTodos();

            var item =
                items.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                items.Remove(item);

                Guardar(items);
            }
        }

        public void Guardar(List<Item> items)
        {
            var json =
                JsonSerializer.Serialize(
                    items,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

            File.WriteAllText(_filePath, json);
        }
    }
}