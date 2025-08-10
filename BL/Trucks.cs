using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Trucks
    {
        private readonly DL.LblancasUsersPruebaTecnicaContext _context;
        public Trucks(DL.LblancasUsersPruebaTecnicaContext context)
        {
            _context = context;
        }

        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                var query = _context.Trucks.FromSqlRaw("TruckGetAll").ToList();
                if(query.Count > 0)
                {
                    foreach(var item in query)
                    {
                        ML.Trucks truck = new ML.Trucks();
                        truck.IdTruck = item.IdTruck;
                        truck.Year = item.Year;
                        truck.Color = item.Color;
                        truck.Plates = item.Plates;
                        result.Objects.Add(truck);
                    }
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontraron Trucks";
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public ML.Result Add(ML.Trucks truck)
        {
            ML.Result result = new ML.Result();
            try
            {
                var year = new SqlParameter("@Year", truck.Year);
                var color = new SqlParameter("@Color", truck.Color);
                var plates = new SqlParameter("@Plates", truck.Plates);
                var query = _context.Database.ExecuteSqlRaw("TruckAdd @Year, @Color, @Plates", year, color, plates);
                if (query > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo agregar la Truck";
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
        public ML.Result Update(ML.Trucks truck)
        {
            ML.Result result = new ML.Result();
            try
            {
                var idTruck = new SqlParameter("@IdTruck", truck.IdTruck);
                var year = new SqlParameter("@Year", truck.Year);
                var color = new SqlParameter("@Color", truck.Color);
                var plates = new SqlParameter("@Plates",truck.Plates);
                var query = _context.Database.ExecuteSqlRaw("TruckUpdate @IdTruck, @Year, @Color, @Plates", idTruck, year, color, plates);
                if (query > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo actualizar la Truck";
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
        public ML.Result Delete(int IdTruck)
        {
            ML.Result result = new ML.Result();
            try
            {
                var idTruck = new SqlParameter("@IdTruck", IdTruck);
                var query = _context.Database.ExecuteSqlRaw("TruckDelete @IdTruck", idTruck);
                if (query > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct= false;
                    result.ErrorMessage = "No se pudo eliminar la Truck";
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
        public ML.Result GetById(int IdTruck)
        {
            ML.Result result = new ML.Result();
            try
            {
                var idTruck = new SqlParameter("@IdTruck", IdTruck);
                var query = _context.Trucks.FromSqlRaw("TruckGetById @IdTruck", idTruck).AsEnumerable().SingleOrDefault();
                if(query != null)
                {
                    ML.Trucks trucks = new ML.Trucks();
                    trucks.IdTruck = query.IdTruck;
                    trucks.Year = query.Year;
                    trucks.Color = query.Color;
                    trucks.Plates = query.Plates;
                    result.Object = trucks;
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontro la Truck";
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
