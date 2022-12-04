using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Inventory.Domains.Models
{
    public class Donaciones
    {
        [ValidateNever]
        [JsonPropertyName("codigo")]
        public int Codigo { get; set; }

        [Required]
        [JsonPropertyName("entidad")]
        public string Entidad { get; set; } = default!;

        [Required]
        [JsonPropertyName("activos")]
        public int Activos { get; set; }
    }
}
