using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderService.Interfaces;
using OrderService.Service;

namespace OrderTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDTO request)
        {
            try
            {
                var result = await _orderService.CreateOrderAsync(request,HttpContext);

                if (result.Success) return Ok(result);

                return BadRequest(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("CancelOrder")]
        public async Task<IActionResult> CancelOrder([FromBody] Cancel cancel)
        {
            try
            {
                var result = await _orderService.CancelOrderAsync(cancel,HttpContext);

                if (result == "Successed")
                    return Ok(result);

                return BadRequest(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

    }
}
