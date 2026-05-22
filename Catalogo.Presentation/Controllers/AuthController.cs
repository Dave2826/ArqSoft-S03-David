using CatalogoApp.Application.Services;
using CatalogoApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApp.Presentation.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
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

                return View();
            }

            HttpContext.Session.SetString(
                "Usuario",
                user.Username
            );

            return RedirectToAction(
                "Index",
                "Catalogo"
            );
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}