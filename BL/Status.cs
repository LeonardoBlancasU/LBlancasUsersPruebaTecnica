using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Status
    {
        private readonly DL.LblancasUsersPruebaTecnicaContext _context;
        public Status(DL.LblancasUsersPruebaTecnicaContext context)
        {
            _context = context;
        }

        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                var query = _context.Statuses.FromSqlRaw("StatusGetAll").ToList();
                if (query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        ML.Status status = new ML.Status();
                        status.IdStatus = item.IdStatus;
                        status.Nombre = item.Nombre;
                        result.Objects.Add(status);
                    }
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontraron Status";
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
