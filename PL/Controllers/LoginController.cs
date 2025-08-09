using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        private readonly BL.Users _BLUsers;
        private readonly IConfiguration _configuration;
        public LoginController(BL.Users BLUsers, IConfiguration configuration)
        {
            _BLUsers = BLUsers;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Login()
        {
            ML.Users login = new ML.Users();
            return View(login);
        }
        [HttpPost]
        public IActionResult Login(ML.Users user)
        {
            ML.Result result = new ML.Result();
            string url = _configuration["AppSettings:Url"] ?? ""; ;
            try
            {
                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(url);

                    var postTask = cliente.PostAsJsonAsync<ML.Users>("Login/Login", user);
                    postTask.Wait();

                    var request = postTask.Result;
                    if (request.IsSuccessStatusCode)
                    {
                        var readTask = request.Content.ReadAsAsync<ML.Result>(); //deserializando Json Result
                        readTask.Wait();

                        string Token = readTask.Result.Object.ToString() ?? "";
                        if (Token != "")
                        {
                            HttpContext.Session.SetString("TokenJWT", Token);
                            return RedirectToAction("GetUser");
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se pudo obtener la informacion";
                        }
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

            return View(user);
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            ML.Users user = new ML.Users();
            string Token = HttpContext.Session.GetString("TokenJWT") ?? "";
            string url = _configuration["AppSettings:Url"] ?? "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
                var responseTask = client.PostAsJsonAsync("Login/MostrarInformacion", Token);
                responseTask.Wait();
                var request = responseTask.Result;
                if (request.IsSuccessStatusCode)
                {
                    var readTask = request.Content.ReadAsAsync<ML.Result>(); //Deserializando Json Result
                    readTask.Wait();
                    user = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Users>(readTask.Result.Object.ToString());
                    HttpContext.Session.SetString("Rol", user?.Rol?.Nombre ?? "");
                }
            }
            return View(user);
        }
    }
}
