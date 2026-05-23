using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;

namespace CatalogoApp.Application.Services
{
    public class ItemService
    {
        private readonly IItemRepository _repository;

        public ItemService(IItemRepository repository)
        {
            _repository = repository;
        }

        public List<Item> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public Item? ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }

        public List<Item> ObtenerPorGenero(string genero)
        {
            var items = _repository.ObtenerTodos();

            if (string.IsNullOrWhiteSpace(genero))
            {
                return items;
            }

            return items
                .Where(x => string.Equals(
                    x.Genero,
                    genero,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<string> ObtenerGeneros()
        {
            return _repository.ObtenerTodos()
                .Select(x => x.Genero)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x)
                .ToList();
        }

        public void Agregar(Item item)
        {
            var items = _repository.ObtenerTodos();

            item.Id = items.Count > 0
                ? items.Max(x => x.Id) + 1
                : 1;

            items.Add(item);

            _repository.Guardar(items);
        }

        public void Eliminar(int id)
        {
            var items = _repository.ObtenerTodos();

            var item =
                items.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                items.Remove(item);

                _repository.Guardar(items);
            }
        }

        public void CambiarFavorito(int id)
        {
            var items = _repository.ObtenerTodos();

            var item =
                items.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                item.Favorito = !item.Favorito;

                _repository.Guardar(items);
            }
        }

        public void ToggleFavorito(int id)
        {
            var items = _repository.ObtenerTodos();

            var item =
                items.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                item.Favorito = !item.Favorito;

                _repository.Guardar(items);
            }
        }
    }
}
