using Inventory.Domains.Models.ViewModels;
using Inventory.Responsive.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventory.Responsive.Controllers
{
    public class DonacionesController : Controller
    {
        readonly IDonacionesServices _donacionesServices;
        readonly IActivosServices _activosServices;

        public DonacionesController(IDonacionesServices donacionesServices, IActivosServices activosServices)
        {
            _donacionesServices = donacionesServices;
            _activosServices = activosServices;
        }

        public async Task<IActionResult> Index()
        {

            var donaciones = await _donacionesServices.getDonacionesAsync();

            return View(donaciones);
        }

        public async Task<IActionResult> Upsert(int? id)
        {

            var activos = await _activosServices.getActivosAsync();

            DonacionesViewModel viewModel = new()
            {
                Donaciones = new(),

                ListaActivos = activos.Select(i => new SelectListItem
                {
                    Text = i.Nombre,
                    Value = i.Codigo.ToString()
                }),
            };

            if (id == null || id == 0)
            {

                return View(viewModel);
            }
            else
            {
                viewModel.Donaciones = await _donacionesServices.getDonacionById((int)id);
                return View(viewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert([FromForm] DonacionesViewModel model)
        {

            if (model.Donaciones.Codigo == 0)
            {
                var donaciones = await _donacionesServices.getDonacionesAsync();

                model.Donaciones.Codigo = donaciones.Count() + 1;

                await _donacionesServices.AddDonacionesAsync(model.Donaciones);
            }
            else
            {
                await _donacionesServices.updateDonacionesAsync(model.Donaciones);
            }

            return RedirectToAction("Index", "Donaciones");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDonaciones(int id)
        {
            await _donacionesServices.deleteDonacionesById(id);
            return RedirectToAction("Index", "Donaciones");

        }
    }
}
