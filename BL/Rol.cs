using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        private readonly DL.LblancasUsersPruebaTecnicaContext _context;
        public Rol(DL.LblancasUsersPruebaTecnicaContext context)
        {
            _context = context;
        }
        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                var query = _context.Rols.FromSqlRaw("RolGetAll").ToList();
                if(query.Count > 0)
                {
                    foreach(var item in query)
                    {
                        ML.Rol rol = new ML.Rol();
                        rol.IdRol = item.IdRol;
                        rol.Nombre = item.Nombre;
                        result.Objects.Add(rol);
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
