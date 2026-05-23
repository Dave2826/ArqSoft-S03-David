using CatalogoApp.Domain.Models;

namespace CatalogoApp.Application.Services
{
    public class FavoriteService
    {
        public List<int> CambiarFavorito(List<int> favoritos, int itemId)
        {
            var resultado = favoritos.Distinct().ToList();

            if (resultado.Contains(itemId))
            {
                resultado.Remove(itemId);
            }
            else
            {
                resultado.Add(itemId);
            }

            return resultado.OrderBy(x => x).ToList();
        }

        public void AplicarEstado(List<Item> items, List<int> favoritos)
        {
            foreach (var item in items)
            {
                item.Favorito = favoritos.Contains(item.Id);
            }
        }
    }
}
