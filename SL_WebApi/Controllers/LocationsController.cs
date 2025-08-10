using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SL_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : Controller
    {
        private readonly BL.Locations _BLLocations;
        public LocationsController(BL.Locations BLLocations)
        {
            _BLLocations = BLLocations;
        }
        [Authorize]
        [HttpGet]
        [Route("GetAll")]

        public IActionResult GetAll()
        {
            ML.Result result = _BLLocations.GetAll();
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

        public IActionResult GetById(int IdLocation)
        {
            ML.Result result = _BLLocations.GetById(IdLocation);
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

        public IActionResult Add([FromBody]ML.Locations location)
        {
            ML.Result result = _BLLocations.Add(location);
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

        public IActionResult Update([FromBody] ML.Locations location)
        {
            ML.Result result = _BLLocations.Update(location);
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

        public IActionResult Delete(int IdLocation)
        {
            ML.Result result = _BLLocations.Delete(IdLocation);
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
