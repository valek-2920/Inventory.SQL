using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Inventory.Domains.Models
{
    public class Usuarios
    {
        [ValidateNever]
        [JsonPropertyName("cedula")]
        public int Cedula { get; set; }

        [Required]
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = default!;

        [Required]
        [JsonPropertyName("primerAppellido")]
        public string PrimerAppellido { get; set; } = default!;

        [Required]
        [JsonPropertyName("segundoApellido")]
        public string SegundoApellido { get; set; } = default!;

        [Required]
        [JsonPropertyName("correo")]
        public string Correo { get; set; } = default!;

        [Required]
        [JsonPropertyName("direccionExacta")]
        public string DireccionExacta { get; set; } = default!;

        [Required]
        [JsonPropertyName("celular")]
        public int Celular { get; set; }
    }
}
