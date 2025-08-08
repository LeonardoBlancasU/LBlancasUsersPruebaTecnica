using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SL_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly BL.Users _BLUsers;

        public UsersController(BL.Users BLUsers)
        {
            _BLUsers = BLUsers;
        }

        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = _BLUsers.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int IdUser)
        {
            ML.Result result = _BLUsers.GetById(IdUser);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody]ML.Users user)
        {
            ML.Result result = _BLUsers.Add(user);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] ML.Users user)
        {
            ML.Result result = _BLUsers.Update(user);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int IdUser)
        {
            ML.Result result = _BLUsers.Delete(IdUser);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
