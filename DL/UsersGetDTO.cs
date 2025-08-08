using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class UsersGetDTO
    {
        public int IdUser { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento {  get; set; }
        public byte[]? Imagen {  get; set; }
        public int IdRol {  get; set; }
        public string NombreRol { get; set; }
    }
}
