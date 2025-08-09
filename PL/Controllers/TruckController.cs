using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PL.Controllers
{
    public class TruckController : Controller
    {
        private readonly IConfiguration _configuration;

        public TruckController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Trucks trucks = new ML.Trucks();
            ML.Result result = GetAllREST();
            if (result.Correct)
            {
                trucks.Camionetas = result.Objects;
            }
            return View(trucks);
        }
        [HttpGet]
        public ActionResult Formulario(int? IdTruck) 
        {
            ML.Trucks trucks = new ML.Trucks();
            ML.Result result = new ML.Result();
            if(IdTruck != null && IdTruck >0) 
            {
                result = GetByIdREST(IdTruck.Value);
                if(result.Correct)
                {
                    trucks = (ML.Trucks)result.Object;
                }
            }
            return View(trucks);
        }
        [HttpPost]
        public IActionResult Formulario(ML.Trucks trucks) 
        {
            if (ModelState.IsValid)
            {
                if(trucks.IdTruck > 0)
                {
                    ML.Result result = UpdateREST(trucks);
                    if (result.Correct)
                    {
                        TempData["Agregado"] = "Truck actualizada correctamente.";
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        TempData["Error"] = "Error al actualizar la Truck: " + result.ErrorMessage;
                    }
                }
                else
                {
                    ML.Result result = AddREST(trucks);
                    if (result.Correct)
                    {
                        TempData["Agregado"] = "Truck agregada correctamente.";
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        TempData["Error"] = "Error al agregar la Truck: " + result.ErrorMessage;
                    }
                }
            }  
            return View(trucks);
        }
        [HttpGet]
        public IActionResult Delete(int IdTruck) 
        {
            ML.Result result = DeleteREST(IdTruck);
            if(result.Correct)
            {
                TempData["Success"] = "Usuario Eliminado Correctamente.";
            }
            else
            {
                TempData["Error"] = " Error al eliminar usuario" + result.ErrorMessage;
            }
            return RedirectToAction("GetAll");
        }

        [NonAction]
        public ML.Result AddREST(ML.Trucks trucks)
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
                    var postTask = client.PostAsJsonAsync<ML.Trucks>("Trucks/Add", trucks); //Serializar
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
        public ML.Result UpdateREST(ML.Trucks trucks)
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
                    var postTask = client.PutAsJsonAsync<ML.Trucks>("Trucks/Update", trucks); //Serializar
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
        public ML.Result DeleteREST(int IdTruck)
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
                    var postTask = client.DeleteAsync("Trucks/Delete?IdTruck=" + IdTruck); //Serializar
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
                    var responseTask = client.GetAsync("Trucks/GetAll");
                    responseTask.Wait();

                    var request = responseTask.Result;
                    if (request.IsSuccessStatusCode)
                    {
                        var readTask = request.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var item in readTask.Result.Objects)
                        {
                            ML.Trucks truck = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Trucks>(item.ToString());
                            result.Objects.Add(truck);
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
        public ML.Result GetByIdREST(int IdTruck)
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
                    var responseTask = client.GetAsync("Trucks/GetById?IdTruck=" + IdTruck);
                    responseTask.Wait();
                    var request = responseTask.Result;
                    if (request.IsSuccessStatusCode)
                    {
                        var readTask = request.Content.ReadAsAsync<ML.Result>(); //Deserializando Json Result
                        readTask.Wait();
                        ML.Trucks truck = new ML.Trucks();
                        truck = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Trucks>(readTask.Result.Object.ToString());
                        result.Object = truck;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro al usuario.";
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
