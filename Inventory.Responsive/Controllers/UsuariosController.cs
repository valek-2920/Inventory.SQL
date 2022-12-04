using Inventory.Responsive.Services.IServices;
using Inventory.Domains.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Responsive.Controllers
{
    public class UsuariosController : Controller
    {

        readonly IUsuariosServices _usuariosServices;

        public UsuariosController(IUsuariosServices usuariosServices)
        {
            _usuariosServices = usuariosServices;
        }

        public async Task<IActionResult> Index()
        {

            var usuarios = await _usuariosServices.getUsuariosAsync();

            return View(usuarios);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            var model = new Usuarios();

            if(id == null)
            {

                return View(model);

            }
            else
            {
                model = await _usuariosServices.getUsuarioById((int)id);

                return View(model);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert([FromForm] Usuarios model)
        {

            if(model.Cedula == 0)
            {
                var usuarios = await _usuariosServices.getUsuariosAsync();

                model.Cedula = usuarios.Count() + 1;

                await _usuariosServices.AddUsuariosAsync(model);
            }
            else
            {
                await _usuariosServices.updateUsuariosAsync(model);
            }

            return RedirectToAction("Index", "Usuarios");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            await _usuariosServices.deleteUsuariosById(id);
            return RedirectToAction("Index", "Usuarios");
        }
    }
}
