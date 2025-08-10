using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Orders
    {
        private readonly DL.LblancasUsersPruebaTecnicaContext _context;
        public Orders(DL.LblancasUsersPruebaTecnicaContext context)
        {
            _context = context;
        }

        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                var query = _context.GetAllOrdersDTO.FromSqlRaw("OrdersGetAll").ToList();
                if(query.Count > 0)
                {
                    foreach(var item in query)
                    {
                        ML.Orders orders = new ML.Orders();
                        orders.IdOrder = item.IdOrders;
                        //orders.User = new ML.User();
                        //orders.User.Nombre = item.Nombre;
                        //orders.User.ApellidoPaterno = item.ApellidoPaterno;
                        //orders.User.ApellidoMaterno = item.ApellidoMaterno;
                        //orders.Truck = new ML.Truck();
                        //orders.Truck.Year = item.Year;
                        //orders.Truck.Color = item.Color;
                        //orders.Truck.Plates = item.Plates;
                        orders.Status = new ML.Status();
                        orders.Status.IdStatus = item.IdStatus;
                        orders.Status.Nombre = item.NombreStatus;
                        //order.LocationPick = new ML.Location();
                        //order.LocationPick.address = item.AddressPick;
                        //order.LocationPick.place_id = item.PlaceIdPick;
                        //order.LocationPick.latitude = item.LatitudePick;
                        //order.LocationPick.longitude = item.LongitudePick;
                        //order.LocationDrop = new ML.Location();
                        //order.LocationDrop.address = item.AddressDrop;
                        //order.LocationDrop.place_id = item.PlaceIdDrop;
                        //order.LocationDrop.laitude = item.LatitudeDrop;
                        //order.LocationDrop.longitude = item.LongitudeDrop;
                        result.Objects.Add(orders);
                    }
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontraron Orders";
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

        public ML.Result Add(ML.Orders order)
        {
            ML.Result result = new ML.Result();

            try
            {
                var idUser = new SqlParameter("@IdUser", order.User.IdUser);
                var idStatus = new SqlParameter("@IdStatus", order.Status.IdStatus);
                var idTruck = new SqlParameter("@IdTruck", order.Truck.IdTruck);
                var idPickUp = new SqlParameter("@IdPickUp", order.LocationPick.IdLocation);
                var idDropOff = new SqlParameter("@IdDropOff", order.LocationDrop.IdLocation);
                var query = _context.Database.ExecuteSqlRaw("OrdersAdd @IdUser, @IdTruck, @IdStatus, @IdPickUp, @IdDropOff", idUser, idTruck, idStatus, idPickUp, idDropOff);
                if (query > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo agregar la Order";
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
        public ML.Result Update(ML.Orders order)
        {
            ML.Result result = new ML.Result();
            try
            {
                var idOrder = new SqlParameter("@IdOrder", order.IdOrder);
                var idUser = new SqlParameter("@IdUser", order.User.IdUser);
                var idStatus = new SqlParameter("@IdStatus", order.Status.IdStatus);
                var idTruck = new SqlParameter("@IdTruck", order.Truck.IdTruck);
                var idPickUp = new SqlParameter("@IdPickUp", order.LocationPick.IdLocation);
                var idDropOff = new SqlParameter("@IdDropOff", order.LocationDrop.IdLocation);
                var query = _context.Database.ExecuteSqlRaw("OrdersUpdate @IdOrder, @IdUser, @IdTruck, @IdStatus, @IdPickUp, @IdDropOff", idOrder, idUser, idTruck, idStatus, idPickUp, idDropOff);
                if (query > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo agregar la Order";
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
        public ML.Result Delete(int IdOrder)
        {
            ML.Result result = new ML.Result();

            try
            {
                var idOrder = new SqlParameter("@IdOrder", IdOrder);
                var query = _context.Database.ExecuteSqlRaw("OrdersDelete @IdOrder", idOrder);
                if(query > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo eliminar la Order";
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
        public ML.Result GetById(int IdOrder)
        {
            ML.Result result = new ML.Result();

            try
            {
                var idOrder = new SqlParameter("@IdOrder", IdOrder);
                var query = _context.Orders.FromSqlRaw("OrdersGetById @IdOrder", idOrder).AsEnumerable().SingleOrDefault() ;
                if(query != null)
                {
                    ML.Orders order = new ML.Orders();
                    order.IdOrder = query.IdOrder;
                    //order.User = new ML.User();
                    //order.User.IdUser = query.IdUser;
                    //order.Truck = new ML.Truck();
                    //order.Truck.IdTruck = query.IdTruck;
                    //order.Status.IdStatus = query.IdStatus;
                    //order.LocationPick = new ML.Location();
                    //order.LocationPick.IdLocation = query.IdPickUp;
                    //order.LocationDrop = new ML.Location();
                    //order.LocationDrop.IdLocation = query.IdDropOff;
                    result.Object = order;
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se encontrola Order";
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
        public ML.Result UpdateStatus(int IdOrder, int IdStatus)
        {
            ML.Result result = new ML.Result() ;
            try
            {
                var idOrder = new SqlParameter("@IdOrder", IdOrder);
                var idStatus = new SqlParameter("@IdStatus", IdStatus);
                var query = _context.Database.ExecuteSqlRaw("OrdersUpdateStatus @IdOrder, @IdStatus", idOrder, idStatus);
                if (query > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se pudo Actualizar el Status";
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
