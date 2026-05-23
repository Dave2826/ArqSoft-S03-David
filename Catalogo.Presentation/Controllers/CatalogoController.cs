using CatalogoApp.Application.Services;
using CatalogoApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApp.Presentation.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly ItemService _service;

        public CatalogoController(ItemService service)
        {
            _service = service;
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

            return View(item);
        }
        [HttpPost]
        public IActionResult EliminarPost(int id)
        {
            _service.Eliminar(id);

            TempData["Mensaje"] =
                "Juego eliminado correctamente.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult FavoritoPost(int id)
        {
            _service.ToggleFavorito(id);

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

            return RedirectToAction("Index");
        }
        public IActionResult Favorito(int id)
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                return RedirectToAction(
                    "Login",
                    "Auth"
                );
            }

            _service.ToggleFavorito(id);

            return RedirectToAction("Index");
        }
    }
}