using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Inventory.Domains.Models
{
    public class Activos
    {
        [ValidateNever]
        [JsonPropertyName("codigo")]
        public int Codigo { get; set; }

        [Required]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = default!;

        [Required]
        [JsonPropertyName("descripcion")]
        public string Descripcion{ get; set; } = default!;

        [Required]
        [JsonPropertyName("categoriaId")]
        public int CategoriaId { get; set; } = default!;
    }
}
