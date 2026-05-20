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

        // Lista con filtro por marca
        public IActionResult Index(string? marca)
        {
            var items = string.IsNullOrEmpty(marca)
                ? _service.ObtenerTodos()
                : _service.ObtenerPorMarca(marca);

            ViewBag.Marcas = _service.ObtenerMarcas();
            ViewBag.MarcaActual = marca;

            return View(items);
        }

        // Detalle
        public IActionResult Detalle(int id)
        {
            var item = _service.ObtenerPorId(id);

            return item == null
                ? NotFound()
                : View(item);
        }

        // GET
        public IActionResult Agregar()
        {
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult Agregar(Item item)
        {
            _service.Agregar(item);

            return RedirectToAction("Index");
        }

        // Eliminar
        public IActionResult Eliminar(int id)
        {
            _service.Eliminar(id);

            return RedirectToAction("Index");
        }
    }
}