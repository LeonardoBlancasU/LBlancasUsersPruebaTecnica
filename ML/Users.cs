using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Users
    {
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Esta Campo es requerido")]
        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El Nombre Excede los 50 Caracteres")]
        [RegularExpression(@"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)*$", ErrorMessage = "Solo se permiten Letras, además cada Nombre debe iniciar con Mayuscula")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Esta Campo es requerido")]
        [Display(Name = "Apellido Paterno")]
        [MaxLength(50, ErrorMessage = "El Apellido Paterno Excede los 50 Caracteres")]
        [RegularExpression(@"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)*$", ErrorMessage = "Solo se permiten Letras, además cada Apellido debe iniciar con Mayuscula")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "Esta Campo es requerido")]
        [Display(Name = "Apellido Materno")]
        [MaxLength(50, ErrorMessage = "El Apellido Materno Excede los 50 Caracteres")]
        [RegularExpression(@"^([A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)(\s[A-ZÁÉÍÓÚÑ][a-záéíóúñ]+)*$", ErrorMessage = "Solo se permiten Letras, además cada Apellido debe iniciar con Mayuscula")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "Esta Campo es requerido")]
        [Display(Name = "Fecha de Nacimiento")]
        public string FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Correo no valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este Campo es requerido")]
        [Display(Name = "Contraseña")]
        [RegularExpression(@"^(?!.*(\d)\1)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W]).+$", ErrorMessage = "Debe tener una Minuscula, una Mayuscula, un Numero, un Caracter Especial (@$!%*?&), no debe contener numeros repetidos ni consecutivos")]
        [MinLength(8, ErrorMessage = "La Contraseña debe tener al menos 8 caracteres")]
        public string Password { get; set; }

        [Display(Name = "Imagen")]
        public byte[]? Imagen { get; set; }

        public ML.Rol Rol { get; set; }

        public List<object>? Usuarios { get; set; }
    }
}
