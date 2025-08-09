using Azure.Core;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsersController : Controller
    {
        private readonly BL.Users _BLUsers;
        private readonly BL.Rol _BLRol;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public UsersController(BL.Users BLUsers, BL.Rol BLRol, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _BLUsers = BLUsers;
            _BLRol = BLRol;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Users users = new ML.Users();
            ML.Result result = GetAllREST();
            if (result.Correct)
            {
                users.Usuarios = result.Objects ?? new List<object>();
            }
            return View(users);
        }
        [HttpGet]
        public IActionResult Formulario(int? IdUser)
        {
            ML.Users user = new ML.Users();
            user.Rol = new ML.Rol();
            ML.Result result = new ML.Result();
            if(IdUser != null && IdUser > 0)
            {
                var respuesta = GetByIdREST(IdUser.Value);
                if (respuesta.Correct)
                {
                    user = (ML.Users)respuesta.Object;
                }
            }
            result = _BLRol.GetAll();
            user.Rol.Roles = result.Correct ? result.Objects : new List<object>();
            return View(user);  
        }
        [HttpPost]
        public IActionResult Formulario(ML.Users users, IFormFile ImagenFile)
        {
            if (ModelState.IsValid)
            {
                ML.Result result = new ML.Result();
                if (ImagenFile != null && ImagenFile.Length > 0)
                {
                    MemoryStream target = new MemoryStream();
                    ImagenFile.CopyTo(target);
                    byte[] data = target.ToArray();
                    users.Imagen = data;
                }
                else
                {
                    // Si no se selecciona una imagen, puedes asignar una imagen predeterminada
                    string defaultImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Img", "Default.png");
                    byte[] defaultImageData = System.IO.File.ReadAllBytes(defaultImagePath);
                    users.Imagen = defaultImageData;
                }
                if(users.IdUser > 0) //Actualizar Usuario
                {
                    result = _BLUsers.GetEmailUnique(users.Email);
                    if(result.Correct)
                    {

                    }
                    var respuesta = UpdateREST(users);
                    if (respuesta.Correct)
                    {
                        TempData["Agregado"] = "User actualizado correctamente.";
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        TempData["Error"] = "Error al actualizar el user: " + result.ErrorMessage;
                    }
                }
                else
                {
                    var respuesta = UpdateREST(users);
                    if (respuesta.Correct)
                    {
                        TempData["Agregado"] = "User actualizado correctamente.";
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        TempData["Error"] = "Error al actualizar el user: " + result.ErrorMessage;
                    }
                }
            }
            return View(users);
        }
        [HttpDelete]
        public IActionResult Delete(int IdUser)
        {

            return View();
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
                    var responseTask = client.GetAsync("Users/GetAll");
                    responseTask.Wait();

                    var request = responseTask.Result;
                    if (request.IsSuccessStatusCode)
                    {
                        var readTask = request.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var item in readTask.Result.Objects)
                        {
                            ML.Users user = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Users>(item.ToString());
                            result.Objects.Add(user);
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
        public ML.Result AddREST(ML.Users user)
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
                    var postTask = client.PostAsJsonAsync<ML.Users>("Users/Add", user); //Serializar
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
        public ML.Result UpdateREST(ML.Users user)
        {
            ML.Result result = new ML.Result();
            string url = _configuration["AppSettings:Url"] ?? "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var postTask = client.PutAsJsonAsync<ML.Users>("Users/Update", user);
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
        public ML.Result DeleteREST(int IdUser)
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
                    var postTask = client.DeleteAsync("Users/Delete?IdUser=" + IdUser);
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
        public ML.Result GetByIdREST(int IdUser)
        {
            ML.Result result = new ML.Result();
            string url = _configuration["AppSettings:Url"] ?? "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var responseTask = client.GetAsync("Users/GetById?IdUsuario=" + IdUser);
                    responseTask.Wait();
                    var request = responseTask.Result;
                    if (request.IsSuccessStatusCode)
                    {
                        var readTask = request.Content.ReadAsAsync<ML.Result>(); //Deserializando Json Result
                        readTask.Wait();
                        ML.Users user = new ML.Users();
                        user = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Users>(readTask.Result.Object.ToString());
                        result.Object = user;
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
