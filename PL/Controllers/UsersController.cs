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
            ML.Result result = new ML.Result();
            return View();
        }
        [HttpGet]
        public IActionResult Formulario(int? IdUser)
        {
            ML.Result result = new ML.Result();
            if(IdUser != null && IdUser > 0)
            {

            }
            else
            {
                
            }
            return View();
        }
        [HttpPost]
        public IActionResult Formulario(ML.Users users, IFormFile ImagenFile)
        {

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
        public ML.Result UpdateREST(int IdUser, ML.Users user)
        {
            ML.Result result = new ML.Result();
            string url = _configuration["AppSettings:Url"] ?? "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var postTask = client.PutAsJsonAsync<ML.Users>("Usuario/Actualizar/" + IdUser, user);
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
