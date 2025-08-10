using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Trucks
    {

        public int IdTruck { get; set; }

        [Required(ErrorMessage = "Esta Campo es requerido")]
        [Display(Name = "Año")]
        [MaxLength(4, ErrorMessage = "El Año Excede los 4 Digitos")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage ="Solo se permiten Numeros")]
        public string? Year { get; set; }

        [Required(ErrorMessage = "Esta Campo es requerido")]
        [Display(Name = "Color")]
        [RegularExpression(@"^[A-Z][a-z]*$", ErrorMessage = "Solo se permiten Numeros, ademas la primer letra deber Ser Mayuscula")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "Esta Campo es requerido")]
        [Display(Name = "Placas")]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "Solo se permiten Numeros y Letras Mayusculas")]
        public string? Plates { get; set; }

        public List<object>? Camionetas { get; set; }
    }
}
