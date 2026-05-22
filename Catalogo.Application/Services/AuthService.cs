using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;

namespace CatalogoApp.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _repo;

        public AuthService(IUserRepository repo)
        {
            _repo = repo;
        }

        public bool Registrar(User user)
        {
            var existente =
                _repo.ObtenerPorEmail(user.Email);

            if (existente != null)
            {
                return false;
            }

            _repo.Registrar(user);

            return true;
        }

        public User? Login(string email, string password)
        {
            var user =
                _repo.ObtenerPorEmail(email);

            if (user == null)
            {
                return null;
            }

            return user.Password == password
                ? user
                : null;
        }
    }
}