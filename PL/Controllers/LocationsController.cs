using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class LocationsController : Controller
    {
        private readonly IConfiguration _configuration;
        public LocationsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Locations location = new ML.Locations();
            ML.Result result = new ML.Result();
            result = GetAllREST();
            if (result.Correct)
            {
                location.Locaciones = result.Objects;
            }
            return View(location);
        }
        [HttpGet]
        public IActionResult Formulario(int? IdLocation) 
        {
            ViewBag.ApiKey = _configuration["ApiKey:AppSettings"];
            ML.Locations location = new ML.Locations();
            ML.Result result = new ML.Result();
            if(IdLocation != null && IdLocation > 0)
            {
                result = GetByIdREST(IdLocation.Value);
                if(result.Correct)
                {
                    location = (ML.Locations)result.Object;    
                }
            }
            return View(location);
        }
        [HttpPost]
        public IActionResult Formulario(ML.Locations location)
        {
            ML.Result result = new ML.Result();
            if(location.IdLocation > 0)
            {
                result = UpdateREST(location);
                if (result.Correct)
                {
                    TempData["Agregado"] = "Location actualizada correctamente.";
                    return RedirectToAction("GetAll");
                }
                else
                {
                    TempData["Error"] = "Error al actualizar la Location: " + result.ErrorMessage;
                }
            }
            else
            {
                result = AddREST(location);
                if (result.Correct)
                {
                    TempData["Agregado"] = "Usuario agregado correctamente.";
                    return RedirectToAction("GetAll");
                }
                else
                {
                    TempData["Error"] = "Error al agregar el usuario: " + result.ErrorMessage;
                }
            }
            return View(location);
        }
        [HttpGet]
        public IActionResult Delete(int IdLocation)
        {
            ML.Result result = DeleteREST(IdLocation);
            if (result.Correct)
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
                    var responseTask = client.GetAsync("Locations/GetAll");
                    responseTask.Wait();

                    var request = responseTask.Result;
                    if (request.IsSuccessStatusCode)
                    {
                        var readTask = request.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var item in readTask.Result.Objects)
                        {
                            ML.Locations location = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Locations>(item.ToString());
                            result.Objects.Add(location);
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
        public ML.Result AddREST(ML.Locations location)
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
                    var postTask = client.PostAsJsonAsync<ML.Locations>("Locations/Add", location); //Serializar
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
        public ML.Result UpdateREST(ML.Locations location)
        {
            ML.Result result = new ML.Result();
            string url = _configuration["AppSettings:Url"] ?? "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var postTask = client.PutAsJsonAsync<ML.Locations>("Locations/Update", location);
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
        public ML.Result DeleteREST(int IdLocation)
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
                    var postTask = client.DeleteAsync("Locations/Delete?IdLocation=" + IdLocation);
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
        public ML.Result GetByIdREST(int IdLocation)
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
                    var responseTask = client.GetAsync("Locations/GetById?IdLocation=" + IdLocation);
                    responseTask.Wait();
                    var request = responseTask.Result;
                    if (request.IsSuccessStatusCode)
                    {
                        var readTask = request.Content.ReadAsAsync<ML.Result>(); //Deserializando Json Result
                        readTask.Wait();
                        ML.Locations location = new ML.Locations();
                        location = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Locations>(readTask.Result.Object.ToString());
                        result.Object = location;
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
