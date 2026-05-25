using System.Collections.Generic;

namespace CatalogoApp.Domain.Interfaces
{
    public interface IFavoriteRepository
    {
        List<int> ObtenerFavoritos(string usuario);

        void GuardarFavoritos(
            string usuario,
            List<int> favoritos
        );
    }
}