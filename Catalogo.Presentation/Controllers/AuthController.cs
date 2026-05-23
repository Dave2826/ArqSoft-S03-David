using CatalogoApp.Application.Services;
using CatalogoApp.Domain.Models;
using CatalogoApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CatalogoApp.Presentation.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        private readonly JsonFavoriteRepository _favoriteRepository;

        public AuthController(
            AuthService authService,
            IWebHostEnvironment environment)
        {
            _authService = authService;
            _favoriteRepository = new JsonFavoriteRepository(
                Path.Combine(environment.ContentRootPath, "Data"));
        }

        // LOGIN VIEW
        public IActionResult Login()
        {
            return View();
        }

        // REGISTER VIEW
        public IActionResult Register()
        {
            return View();
        }

        // REGISTER POST
        [HttpPost]
        public IActionResult Register(User user)
        {
            var registrado =
                _authService.Registrar(user);

            if (!registrado)
            {
                ViewBag.Error =
                    "El correo ya está registrado.";

                return View();
            }

            TempData["Success"] =
                "Cuenta creada correctamente.";

            return RedirectToAction("Login");
        }

        // LOGIN POST
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user =
                _authService.Login(email, password);

            if (user == null)
            {
                ViewBag.Error =
                    "Correo o contraseña incorrectos.";

                TempData["Error"] =
                    "Correo o contraseña incorrectos.";

                return View();
            }

            HttpContext.Session.SetString(
                "Usuario",
                user.Username
            );

            var favoritosTemporales = ObtenerFavoritosSesion();
            var favoritosUsuario = _favoriteRepository.ObtenerPorUsuario(user.Username);

            var favoritosSincronizados = favoritosUsuario
                .Union(favoritosTemporales)
                .OrderBy(x => x)
                .ToList();

            _favoriteRepository.Guardar(user.Username, favoritosSincronizados);

            HttpContext.Session.SetString(
                "Favoritos",
                JsonSerializer.Serialize(favoritosSincronizados));

            TempData["Success"] =
                $"Bienvenido, {user.Username}.";

            return RedirectToAction(
                "Index",
                "Catalogo"
            );
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            TempData["Info"] =
                "Sesión cerrada correctamente.";

            return RedirectToAction("Login");
        }

        private List<int> ObtenerFavoritosSesion()
        {
            var favoritos = HttpContext.Session.GetString("Favoritos");

            if (string.IsNullOrWhiteSpace(favoritos))
            {
                return new List<int>();
            }

            return JsonSerializer.Deserialize<List<int>>(favoritos)
                ?? new List<int>();
        }
    }
}
