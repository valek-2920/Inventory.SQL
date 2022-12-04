using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventory.Domains.Models.ViewModels
{
    public class DonacionesViewModel
    {
        public Donaciones Donaciones { get; set; } = default!;

        [ValidateNever]
        public IEnumerable<SelectListItem> ListaActivos{ get; set; } = default!;

    }
}
