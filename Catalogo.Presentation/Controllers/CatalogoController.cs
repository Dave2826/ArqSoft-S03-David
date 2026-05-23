using CatalogoApp.Application.Services;
using CatalogoApp.Domain.Models;
using CatalogoApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CatalogoApp.Presentation.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ItemService _service;
        private readonly FavoriteService _favoriteService;
        private readonly JsonFavoriteRepository _favoriteRepository;

        public CatalogoController(
            ItemService service,
            IWebHostEnvironment environment)
        {
            _service = service;
            _favoriteService = new FavoriteService();
            _favoriteRepository = new JsonFavoriteRepository(
                Path.Combine(environment.ContentRootPath, "Data"));
        }

        // LISTA PRINCIPAL
        public IActionResult Index(string? genero)
        {
            var items = string.IsNullOrEmpty(genero)
                ? _service.ObtenerTodos()
                : _service.ObtenerPorGenero(genero);

            ViewBag.Generos =
                _service.ObtenerGeneros();

            ViewBag.GeneroActual =
                genero;

            _favoriteService.AplicarEstado(
                items,
                ObtenerFavoritosActuales());

            return View(items);
        }

        // DETALLE
        public IActionResult Detalle(int id)

        {
            var item =
                _service.ObtenerPorId(id);

            if (item == null)
            {
                return NotFound();
            }

            _favoriteService.AplicarEstado(
                new List<Item> { item },
                ObtenerFavoritosActuales());

            return View(item);
        }

        public IActionResult Favoritos()
        {
            var items = _service.ObtenerTodos();
            var favoritosActuales = ObtenerFavoritosActuales();

            _favoriteService.AplicarEstado(items, favoritosActuales);

            var favoritos = items
                .Where(x => x.Favorito)
                .ToList();

            return View(favoritos);
        }

        public IActionResult Dashboard()
        {
            var items = _service.ObtenerTodos();
            var favoritosActuales = ObtenerFavoritosActuales();

            _favoriteService.AplicarEstado(items, favoritosActuales);

            ViewBag.TotalJuegos = items.Count;
            ViewBag.TotalFavoritos = favoritosActuales.Count;
            ViewBag.GeneroMasUsado = items
                .Where(x => !string.IsNullOrWhiteSpace(x.Genero))
                .GroupBy(x => x.Genero)
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key)
                .FirstOrDefault() ?? "S/N";
            ViewBag.PlataformaMasUsada = items
                .Where(x => !string.IsNullOrWhiteSpace(x.Plataforma))
                .GroupBy(x => x.Plataforma)
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key)
                .FirstOrDefault() ?? "S/N";
            ViewBag.UltimoJuego = items
                .OrderByDescending(x => x.Id)
                .FirstOrDefault()?.Nombre ?? "S/N";

            var ultimosJuegos = items
                .OrderByDescending(x => x.Id)
                .Take(4)
                .ToList();

            return View(ultimosJuegos);
        }

        [HttpPost]
        public IActionResult EliminarPost(int id)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth"
                );
            }

            _service.Eliminar(id);

            TempData["Success"] =
                "Juego eliminado correctamente.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult FavoritoPost(int id, string? returnUrl)
        {
            var favoritos = _favoriteService.CambiarFavorito(
                ObtenerFavoritosActuales(),
                id);

            GuardarFavoritosActuales(favoritos);

            if (favoritos.Contains(id))
            {
                TempData["Success"] = "Juego agregado a favoritos.";
            }
            else
            {
                TempData["Info"] = "Juego quitado de favoritos.";
            }

            if (!string.IsNullOrWhiteSpace(returnUrl)
                && Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction("Index");
        }

        // GET AGREGAR
        public IActionResult Agregar()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth"
                );
            }

            return View();
        }

        // POST AGREGAR
        [HttpPost]
        public IActionResult Agregar(Item item)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth"
                );
            }

            item.Desarrollador =
                string.IsNullOrWhiteSpace(item.Desarrollador)
                ? "S/N"
                : item.Desarrollador;

            item.Genero =
                string.IsNullOrWhiteSpace(item.Genero)
                ? "S/N"
                : item.Genero;

            item.Descripcion =
                string.IsNullOrWhiteSpace(item.Descripcion)
                ? "S/N"
                : item.Descripcion;

            item.Plataforma =
                string.IsNullOrWhiteSpace(item.Plataforma)
                ? "S/N"
                : item.Plataforma;

            item.Duracion =
                string.IsNullOrWhiteSpace(item.Duracion)
                ? "S/N"
                : item.Duracion;

            item.Clasificacion =
                string.IsNullOrWhiteSpace(item.Clasificacion)
                ? "S/N"
                : item.Clasificacion;

            item.ImagenUrl =
                string.IsNullOrWhiteSpace(item.ImagenUrl)
                ? "/images/games/default.jpg"
                : item.ImagenUrl;

            _service.Agregar(item);

            TempData["Success"] =
                "Juego agregado correctamente.";

            return RedirectToAction("Index");
        }

        // ELIMINAR
        public IActionResult Eliminar(int id)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth"
                );
            }

            _service.Eliminar(id);

            TempData["Success"] =
                "Juego eliminado correctamente.";

            return RedirectToAction("Index");
        }
        public IActionResult Favorito(int id)
        {
            var favoritos = _favoriteService.CambiarFavorito(
                ObtenerFavoritosActuales(),
                id);

            GuardarFavoritosActuales(favoritos);

            if (favoritos.Contains(id))
            {
                TempData["Success"] = "Juego agregado a favoritos.";
            }
            else
            {
                TempData["Info"] = "Juego quitado de favoritos.";
            }

            return RedirectToAction("Index");
        }

        private List<int> ObtenerFavoritosActuales()
        {
            var favoritosSesion = HttpContext.Session.GetString("Favoritos");

            if (!string.IsNullOrWhiteSpace(favoritosSesion))
            {
                return JsonSerializer.Deserialize<List<int>>(favoritosSesion)
                    ?? new List<int>();
            }

            var usuario = HttpContext.Session.GetString("Usuario");

            if (!string.IsNullOrWhiteSpace(usuario))
            {
                var favoritosUsuario = _favoriteRepository.ObtenerPorUsuario(usuario);
                GuardarFavoritosEnSesion(favoritosUsuario);

                return favoritosUsuario;
            }

            return new List<int>();
        }

        private void GuardarFavoritosActuales(List<int> favoritos)
        {
            GuardarFavoritosEnSesion(favoritos);

            var usuario = HttpContext.Session.GetString("Usuario");

            if (!string.IsNullOrWhiteSpace(usuario))
            {
                _favoriteRepository.Guardar(usuario, favoritos);
            }
        }

        private void GuardarFavoritosEnSesion(List<int> favoritos)
        {
            HttpContext.Session.SetString(
                "Favoritos",
                JsonSerializer.Serialize(favoritos));
        }
    }
}
