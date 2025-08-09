using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SL_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly BL.Orders _BLOrders;
        public OrdersController(BL.Orders BLOrders)
        {
            _BLOrders = BLOrders;
        }
        [Authorize]
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = _BLOrders.GetAll();
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
        public IActionResult GetById(int IdOrder)
        {
            ML.Result result = _BLOrders.GetById(IdOrder);
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
        public IActionResult Add([FromBody] ML.Orders order)
        {
            ML.Result result = _BLOrders.Add(order);
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
        public IActionResult Update([FromBody] ML.Orders order)
        {
            ML.Result result = _BLOrders.Update(order);
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
        public IActionResult Delete(int IdOrder)
        {
            ML.Result result = _BLOrders.Delete(IdOrder);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpDelete]
        [Route("UpdateStatus")]
        public IActionResult UpdateStatus(int IdOrder, int IdStatus)
        {
            ML.Result result = _BLOrders.UpdateStatus(IdOrder, IdStatus);
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
