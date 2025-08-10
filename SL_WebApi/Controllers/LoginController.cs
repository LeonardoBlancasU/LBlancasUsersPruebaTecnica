using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SL_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly BL.Users _BLUsers;
        public LoginController(BL.Users BLUsers)
        {
            _BLUsers = BLUsers;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] ML.Login login)
        {
            ML.Result result = _BLUsers.Login(login);
            if (result.Correct)
            {
                int IdUser = (int)result.Object;
                ML.Result resultUser = _BLUsers.GetById(IdUser);
                ML.Users user = (ML.Users)resultUser.Object;
                
                ML.Result resultToken = GenerarToken(user);

                if (resultToken.Correct)
                {
                    return Ok(resultToken);
                }
                //Generar Token
                return Ok("TOKEN");
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [NonAction]
        public ML.Result GenerarToken([FromBody] ML.Users user)
        {
            ML.Result result = new ML.Result();
            try
            {
                var issuer = "localhost";
                var audience = "localhost";
                var key = "S3cr3t_K3y!.123_S3cr3t_K3y!.123d@%E";
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                var claims = new[]
                {
                     new Claim("Nombre", $"{user.Nombre}"),
                     new Claim("IdUser", $"{user.IdUser}"),
                     new Claim(ClaimTypes.Role, $"{user.Rol.Nombre}"),
                     new Claim("FechaNacimiento", user.FechaNacimiento.ToString()),
                     new Claim("IdRol", $"{user.Rol.IdRol}"),
                     new Claim("ApellidoMaterno", $"{user.ApellidoMaterno}"),
                     new Claim("ApellidoPaterno", $"{user.ApellidoPaterno}"),
                };
                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: credentials
                    );
                string TokenGenerado = new JwtSecurityTokenHandler().WriteToken(token);
                result.Object = TokenGenerado;
                result.Correct = true;

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        [Authorize]
        [HttpPost]
        [Route("MostrarInformacion")]
        public ML.Result ObtenerInformacion([FromBody] string Token)
        {
            ML.Result result = new ML.Result();
            ML.Users user = new ML.Users();
            user.Rol = new ML.Rol();
            var asignar = new JwtSecurityTokenHandler();
            var jwtToken = asignar.ReadJwtToken(Token);
            var claims = jwtToken.Claims;
            user.Nombre = claims.FirstOrDefault(c => c.Type == "Nombre")?.Value ?? "";
            user.ApellidoPaterno = claims.FirstOrDefault(c => c.Type == "ApellidoPaterno")?.Value ?? "";
            user.ApellidoMaterno = claims.FirstOrDefault(c => c.Type == "ApellidoMaterno")?.Value ?? "";
            user.IdUser = Convert.ToInt32(claims.FirstOrDefault(c => c.Type == "IdUser")?.Value);
            user.FechaNacimiento = claims.FirstOrDefault(c => c.Type == "FechaNacimiento")?.Value ?? "";
            user.Rol.IdRol = Convert.ToByte(claims.FirstOrDefault(c => c.Type == "IdRol")?.Value);
            user.Rol.Nombre = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "";
            result.Object = user;
            return result;
        }
    }
}
