
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Inventory.Domains.Models
{
    public class Categoria
    {
        [ValidateNever]
        [JsonPropertyName("codigo")]
        public int Codigo { get; set; }

        [Required]
        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = default!;
    }
}
