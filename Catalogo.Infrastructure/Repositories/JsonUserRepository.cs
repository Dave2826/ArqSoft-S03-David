using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Domain.Models;
using System.Text.Json;

namespace CatalogoApp.Infrastructure.Repositories
{
    public class JsonUserRepository : IUserRepository
    {
        private readonly string _filePath;

        public JsonUserRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public List<User> ObtenerTodos()
        {
            var json = File.ReadAllText(_filePath);

            return JsonSerializer.Deserialize<List<User>>(json)
                   ?? new List<User>();
        }

        public User? ObtenerPorEmail(string email)
        {
            return ObtenerTodos()
                .FirstOrDefault(u => u.Email == email);
        }

        public void Registrar(User user)
        {
            var users = ObtenerTodos();

            user.Id = users.Any()
                ? users.Max(u => u.Id) + 1
                : 1;

            users.Add(user);

            var json = JsonSerializer.Serialize(
                users,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(_filePath, json);
        }
    }
}