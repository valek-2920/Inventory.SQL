using Inventory.Domains.Models;
using Inventory.Responsive.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Responsive.Controllers
{
    public class CategoriaController : Controller
    {
        readonly ICategoriasServices _categoriasServices;

        public CategoriaController(ICategoriasServices categoriasServices)
        {
            _categoriasServices = categoriasServices;
        }

        public async Task<IActionResult> Index()
        {

            var categorias = await _categoriasServices.getCategoriasAsync();

            return View(categorias);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var model = new Categoria();

            if (id == null)
            {

                return View(model);

            }
            else
            {
                model = await _categoriasServices.getCategoriaById((int)id);

                return View(model);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert([FromForm] Categoria model)
        {

            if (model.Codigo == 0)
            {
                var categorias = await _categoriasServices.getCategoriasAsync();

                model.Codigo = categorias.Count() + 1;

                await _categoriasServices.AddCategoriasAsync(model);
            }
            else
            {
                await _categoriasServices.updateCategoriasAsync(model);
            }

            return RedirectToAction("Index", "Categoria");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            await _categoriasServices.deleteCategoriasById(id);
            return RedirectToAction("Index", "Categoria");

        }
    }
}
