using Azure.Core;
using DL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Data;

namespace PL.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly BL.Users _BLUsers;
        private readonly BL.Trucks _BLTrucks;
        private readonly BL.Status _BLStatus;
        private readonly BL.Locations _BLLocationsPick;
        private readonly BL.Locations _BLLocationsDrop;
        public OrdersController(IConfiguration configuration, BL.Users BLUsers, BL.Trucks BLTrucks, BL.Status BLStatus, BL.Locations BLLocationsPick, BL.Locations BLLocationsDrop)
        {
            _configuration = configuration;
            _BLUsers = BLUsers;
            _BLStatus = BLStatus;
            _BLTrucks = BLTrucks;
            _BLStatus = BLStatus;
            _BLLocationsPick = BLLocationsPick;
            _BLLocationsDrop = BLLocationsDrop;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            ViewBag.UpdateStatus = _configuration["AppSettings:UrlUpdateStatus"];
            ML.Orders orders = new ML.Orders();
            orders.Status = new ML.Status();
            ML.Result result = new ML.Result();
            result = GetAllREST();
            if(result.Correct)
            {
                orders.Ordenes = result.Objects;
            }
            ML.Result resultStatus = _BLStatus.GetAll();
            orders.Status.Statues = resultStatus.Correct ? resultStatus.Objects : new List<object>();
            return View(orders);
        }
        [HttpGet]
        public ActionResult Formulario(int? IdOrder) 
        {
            ML.Orders order = new ML.Orders();
            order.User =new ML.Users();
            order.Status = new ML.Status();
            order.Truck = new ML.Trucks();
            order.LocationPick = new ML.Locations();
            order.LocationDrop = new ML.Locations();
            if(IdOrder != null && IdOrder > 0)
            {
                var result = GetByIdREST(IdOrder.Value);
                if (result.Correct)
                {
                    order = (ML.Orders)result.Object;
                }
            }
            ML.Result resultStatus = _BLStatus.GetAll();
            order.Status.Statues = resultStatus.Correct ? resultStatus.Objects : new List<object>();
            ML.Result resultUsers = _BLUsers.GetAll();
            order.User.Usuarios = resultUsers.Correct ? resultUsers.Objects : new List<object>();
            ML.Result resultTrucks = _BLTrucks.GetAll();
            order.Truck.Camionetas = resultTrucks.Correct ? resultTrucks.Objects : new List<object>();
            ML.Result resultPicks = _BLLocationsPick.GetAll();
            order.LocationPick.Locaciones = resultPicks.Correct ? resultPicks.Objects : new List<object>();
            ML.Result resultDrops = _BLLocationsDrop.GetAll();
            order.LocationDrop.Locaciones = resultDrops.Correct ? resultDrops.Objects : new List<object>();
            return View(order);
        }
        [HttpPost]
        public IActionResult Formulario(ML.Orders order)
        {
  
            if (order.User.IdUser > 0 && order.Truck.IdTruck > 0 && order.LocationDrop.IdLocation > 0 && order.LocationPick.IdLocation >0)
            {
                if(order.IdOrder > 0)
                {
                    ML.Result result = UpdateREST(order);
                    if (result.Correct)
                    {
                        TempData["Agregado"] = "Order actualizada correctamente.";
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        TempData["Error"] = "Error al actualizar la Order: " + result.ErrorMessage;
                    }
                }
                else
                {
                    order.Status = new ML.Status();
                    order.Status.IdStatus = 1;
                    ML.Result result = AddREST(order);
                    if (result.Correct)
                    {
                        TempData["Agregado"] = "Order agregada correctamente.";
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        TempData["Error"] = "Error al agregar la Order: " + result.ErrorMessage;
                    }
                }
            }
            else
            {
                TempData["Error"] = "Favor de Seleccionar todo.";
            }
            //Llenar DDLS
            ML.Result resultUsers = _BLUsers.GetAll();
            order.User.Usuarios = resultUsers.Correct ? resultUsers.Objects : new List<object>();
            ML.Result resultTrucks = _BLTrucks.GetAll();
            order.Truck.Camionetas = resultTrucks.Correct ? resultTrucks.Objects : new List<object>();
            ML.Result resultPicks = _BLLocationsPick.GetAll();
            order.LocationPick.Locaciones = resultPicks.Correct ? resultPicks.Objects : new List<object>();
            ML.Result resultDrops = _BLLocationsDrop.GetAll();
            order.LocationDrop.Locaciones = resultDrops.Correct ? resultDrops.Objects : new List<object>();
            return View(order);
        }
        [HttpGet]
        public IActionResult Delete(int IdOrder) {
            ML.Result result = DeleteREST(IdOrder);
            if (result.Correct)
            {
                TempData["Success"] = "Order Eliminado Correctamente.";
            }
            else
            {
                TempData["Error"] = " Error al eliminar la Order" + result.ErrorMessage;
            }
            return RedirectToAction("GetAll");
        }

        [NonAction]
        public ML.Result GetAllREST()
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            string url = _configuration["AppSettings:Url"] ?? "";
            string token = HttpContext.Session.GetString("TokenJWT") ?? "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var responseTask = client.GetAsync("Orders/GetAll");
                    responseTask.Wait();

                    var request = responseTask.Result;
                    if (request.IsSuccessStatusCode)
                    {
                        var readTask = request.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var item in readTask.Result.Objects)
                        {
                            ML.Orders order = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Orders>(item.ToString());
                            result.Objects.Add(order);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = request.StatusCode.ToString();
                    }
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
        [NonAction]
        public ML.Result AddREST(ML.Orders order)
        {
            ML.Result result = new ML.Result();
            string url = _configuration["AppSettings:Url"] ?? "";
            string token = HttpContext.Session.GetString("TokenJWT") ?? "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    //HTTP POST 
                    var postTask = client.PostAsJsonAsync<ML.Orders>("Orders/Add", order); //Serializar
                    postTask.Wait();

                    var request = postTask.Result;
                    if (request.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = request.StatusCode.ToString();
                    }
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

        [NonAction]
        public ML.Result UpdateREST(ML.Orders order)
        {
            ML.Result result = new ML.Result();
            string url = _configuration["AppSettings:Url"] ?? "";
            string token = HttpContext.Session.GetString("TokenJWT") ?? "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var postTask = client.PutAsJsonAsync<ML.Orders>("Orders/Update", order);
                    postTask.Wait();

                    var request = postTask.Result;
                    if (request.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
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

        [NonAction]
        public ML.Result DeleteREST(int IdOrder)
        {
            ML.Result result = new ML.Result();
            string url = _configuration["AppSettings:Url"] ?? "";
            string token = HttpContext.Session.GetString("TokenJWT") ?? "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    //HTTP PUT
                    var postTask = client.DeleteAsync("Orders/Delete?IdOrder=" + IdOrder);
                    postTask.Wait();

                    var request = postTask.Result;
                    if (request.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = request.StatusCode.ToString();
                    }
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

        [NonAction]
        public ML.Result GetByIdREST(int IdOrder)
        {
            ML.Result result = new ML.Result();
            string url = _configuration["AppSettings:Url"] ?? "";
            string token = HttpContext.Session.GetString("TokenJWT") ?? "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var responseTask = client.GetAsync("Orders/GetById?IdOrder=" + IdOrder);
                    responseTask.Wait();
                    var request = responseTask.Result;
                    if (request.IsSuccessStatusCode)
                    {
                        var readTask = request.Content.ReadAsAsync<ML.Result>(); //Deserializando Json Result
                        readTask.Wait();
                        ML.Orders order = new ML.Orders();
                        order = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Orders>(readTask.Result.Object.ToString());
                        result.Object = order;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro la Order.";
                    }
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
