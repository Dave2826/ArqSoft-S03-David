using CatalogoApp.Domain.Interfaces;

namespace CatalogoApp.Domain.Interfaces
{
    public class JsonFavoriteRepository : IFavoriteRepository
    {
        List<int> ObtenerFavoritos(string usuario);

        void GuardarFavoritos(
            string usuario,
            List<int> favoritos
        );
    }
}