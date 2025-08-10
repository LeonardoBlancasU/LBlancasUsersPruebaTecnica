using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SL_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrucksController : Controller
    {
        private readonly BL.Trucks _BLTrucks;
        public TrucksController(BL.Trucks BLTrucks)
        {
            _BLTrucks = BLTrucks;
        }
        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = _BLTrucks.GetAll();
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
        public IActionResult GetById(int IdTruck)
        {
            ML.Result result = _BLTrucks.GetById(IdTruck);
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
        public IActionResult Add([FromBody]ML.Trucks truck)
        {
            ML.Result result = _BLTrucks.Add(truck);
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
        public IActionResult Delete(int IdTruck)
        {
            ML.Result result = _BLTrucks.Delete(IdTruck);
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
        public IActionResult Update([FromBody] ML.Trucks truck)
        {
            ML.Result result = _BLTrucks.Update(truck);
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
