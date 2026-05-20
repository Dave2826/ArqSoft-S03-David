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

        // FILTRO POR MARCA
        public List<Item> ObtenerPorMarca(string marca)
        {
            return _repo.ObtenerTodos()
                        .Where(i => i.Marca == marca)
                        .ToList();
        }

        public List<string> ObtenerMarcas()
        {
            return _repo.ObtenerTodos()
                        .Select(i => i.Marca)
                        .Distinct()
                        .ToList();
        }
    }
}