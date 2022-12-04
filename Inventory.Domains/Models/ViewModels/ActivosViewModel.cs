using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventory.Domains.Models.ViewModels
{
    public class ActivosViewModel
    {
        public Activos Activo { get; set; } = default!;

        [ValidateNever]
        public IEnumerable<SelectListItem> ListaCategorias { get; set; } = default!;

    }
}
