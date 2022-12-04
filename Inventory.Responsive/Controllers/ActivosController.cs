using Inventory.Domains.Models.ViewModels;
using Inventory.Responsive.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventory.Responsive.Controllers
{
    public class ActivosController : Controller
    {
        readonly IActivosServices _activosServices; 
        readonly ICategoriasServices _categoriasServices;

        public ActivosController(IActivosServices activosServices, ICategoriasServices categoriasServices)
        {
            _activosServices = activosServices;
            _categoriasServices = categoriasServices;
        }

        public async Task<IActionResult> Index()
        {

            var activos = await _activosServices.getActivosAsync();

            return View(activos);
        }

        public async Task<IActionResult> Upsert(int? id)
        {

            var categorias = await _categoriasServices.getCategoriasAsync();

            ActivosViewModel viewModel = new()
            {
                Activo = new(),

                ListaCategorias = categorias.Select(i => new SelectListItem
                {
                    Text = i.Descripcion,
                    Value = i.Codigo.ToString()
                }),
            };

            if (id == null || id == 0)
            {

                return View(viewModel);

            }
            else
            {
                viewModel.Activo = await _activosServices.getActivoById((int)id);
                return View(viewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert([FromForm] ActivosViewModel model)
        {

            if (model.Activo.Codigo == 0)
            {
                var activos = await _activosServices.getActivosAsync();

                model.Activo.Codigo = activos.Count() + 1;

                await _activosServices.AddActivosAsync(model.Activo);
            }
            else
            {
                await _activosServices.updateActivosAsync(model.Activo);
            }

            return RedirectToAction("Index", "Activos");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteActivo(int id)
        {
            await _activosServices.deleteActivosById(id);
            return RedirectToAction("Index", "Activos");
        }
    }
}
