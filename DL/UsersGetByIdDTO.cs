using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class UsersGetByIdDTO
    {
        public int IdUser { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string FechaNacimiento { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public byte[]? Imagen { get; set; }
        public byte IdRol { get; set; }
        public string NombreRol { get; set; }
    }
}
