using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;

namespace CatalogoApp.Application.Services
{
    public class ItemService
    {
        private readonly IItemRepository _repo;

        public ItemService(IItemRepository repo)
        {
            _repo = repo;
        }

        public List<Item> ObtenerTodos()
        {
            return _repo.ObtenerTodos();
        }

        public Item? ObtenerPorId(int id)
        {
            return _repo.ObtenerPorId(id);
        }

        public void Agregar(Item item)
        {
            _repo.Agregar(item);
        }

        public void Eliminar(int id)
        {
            _repo.Eliminar(id);
        }

        // Filtro por género
        public List<Item> ObtenerPorGenero(string genero)
        {
            return _repo.ObtenerTodos()
                        .Where(i => i.Genero == genero)
                        .ToList();
        }

        public List<string> ObtenerGeneros()
        {
            return _repo.ObtenerTodos()
                        .Select(i => i.Genero)
                        .Distinct()
                        .ToList();
        }
    }
}