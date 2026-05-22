using CatalogoApp.Domain.Models;

namespace CatalogoApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        List<User> ObtenerTodos();

        User? ObtenerPorEmail(string email);

        void Registrar(User user);
    }
}