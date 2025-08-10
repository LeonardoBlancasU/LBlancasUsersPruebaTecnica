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
    public class Locations
    {
        private readonly DL.LblancasUsersPruebaTecnicaContext _context;
        public Locations(DL.LblancasUsersPruebaTecnicaContext context)
        {
            _context = context;
        }
        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                var query = _context.Locations.FromSqlRaw("LocationGetAll").ToList();
                if(query.Count>0)
                {
                    foreach(var item in query)
                    {
                        ML.Locations location = new ML.Locations();
                        location.IdLocation = item.IdLocation;
                        location.address = item.Address;
                        location.place_id = item.PlaceId;
                        location.latitude = item.Latitude;
                        location.longitude = item.Longitude;
                        result.Objects.Add(location);
                    }
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontraro Locations";
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
        public ML.Result Add(ML.Locations location)
        {
            ML.Result result = new ML.Result();
            try
            {
                var Address = new SqlParameter("@address", location.address);
                var PlaceId = new SqlParameter("@place_id", location.place_id);
                var Latitude = new SqlParameter("@latitude", location.latitude);
                var Longitude = new SqlParameter("@longitude", location.longitude);
                var query = _context.Database.ExecuteSqlRaw("LocationAdd @address, @place_id, @latitude, @longitude", Address, PlaceId, Latitude, Longitude);
                if (query > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo agregar la Location";
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
        public ML.Result Update(ML.Locations location)
        {
            ML.Result result = new ML.Result();
            try
            {   
                var idLocation = new SqlParameter("@IdLocation", location.IdLocation);
                var Address = new SqlParameter("@address", location.address);
                var PlaceId = new SqlParameter("@place_id", location.place_id);
                var Latitude = new SqlParameter("@latitude", location.latitude);
                var Longitude = new SqlParameter("@longitude", location.longitude);
                var query = _context.Database.ExecuteSqlRaw("LocationUpdate @IdLocation, @address, @place_id, @latitude, @longitude", idLocation, Address, PlaceId, Latitude, Longitude);
                if (query > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo actualizar la Location";
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
        public ML.Result Delete(int IdLocation)
        {
            ML.Result result = new ML.Result();
            try
            {
                var idLocation = new SqlParameter("@IdLocation", IdLocation);
                var query = _context.Database.ExecuteSqlRaw("LocationDelete @IdLocation", idLocation);
                if (query > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo eliminar la Location";
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
        public ML.Result GetById(int IdLocation)
        {
            ML.Result result = new ML.Result();
            try
            {
                var idLocation = new SqlParameter("@IdLocation", IdLocation);
                var query = _context.Locations.FromSqlRaw("LocationGetById @IdLocation", idLocation).AsEnumerable().SingleOrDefault();
                if (query != null)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontro la Location";
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
