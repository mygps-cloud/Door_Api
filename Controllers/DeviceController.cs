using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Models.DeviceModel;
using WebAPI.Services.DeviceService;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _DeviceService;

        public DeviceController(IDeviceService DeviceService)
        {
            _DeviceService = DeviceService;
        }
        [Route("GetDevices")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetAllDevices()
        {
	        try
	        {
		        var result = await _DeviceService.GetAllDevices();
		        return Ok(result);
			}
	        catch (ArgumentException e)
	        {
		        return NotFound(e.Message);
	        }
          
        }


        [Route("GetOrders")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetAllOrders()
        {
			try
			{
				var result = await _DeviceService.GetAllDevices();
				return Ok(result);
			}
			catch (ArgumentException e)
			{
				return NotFound(e.Message);
			}
		}

        [Route("GetOrderHistory")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetOrderHistory()
        {
			try
			{
				var result = await _DeviceService.GetAllDevices();
				return Ok(result);
			}
			catch (ArgumentException e)
			{
				return NotFound(e.Message);
			}
		}

        [Route("GetListenerInfo")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> GetListenerInfo()
        {
			try
			{
				var result = await _DeviceService.GetAllDevices();
				return Ok(result);
			}
			catch (ArgumentException e)
			{
				return NotFound(e.Message);
			}
		}



  //      [Route("AddOrder")]
  //      [HttpPost]
		//[ProducesResponseType(StatusCodes.Status200OK)]
  //      [ProducesResponseType(StatusCodes.Status400BadRequest)]
		//public async Task<IActionResult> AddOrder(OrderModel order)
  //      {
  //          var result = await _DeviceService.AddOrder(order);
  //          if (result == null)
	 //           return BadRequest(400);
  //          return Ok(result);
  //      }

        [Route("OrderHistory")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]

        public async Task<IActionResult> AddOrderHistory(OrderHistory order)
        {
            try
            {
                var result = await _DeviceService.AddOrderHistory(order);
                return Ok(result);
            }
            catch (DbUpdateException e)
            {
                return StatusCode(500, e.Message);
            }
            catch (TimeoutException e)
            {
                return StatusCode(408, e.Message);
            }
        }

		//[Route("AddListenerModel")]
		//[HttpPost]
		//public async Task<IActionResult> AddListenerModel(ListenerModel order)
		//{
		//    var result = await _DeviceService.AddListenerModel(order);
		//    return Ok(result);
		//}
		//      [Route("AddDevice")]
		//      [HttpPost]
		//      [ProducesResponseType(StatusCodes.Status200OK)]
		//      [ProducesResponseType(StatusCodes.Status400BadRequest)]
		//public async Task<IActionResult> AddDevice(DeviceModel device)
		//      {
		//          var result = await _DeviceService.AddDevice(device);
		//          if (result.Count == 0)
		//              return BadRequest("Device already existed");

		//              return Ok(result);
		//      }
	}
}