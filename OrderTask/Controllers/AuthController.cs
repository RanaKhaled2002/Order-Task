using Microsoft.AspNetCore.Mvc;
using OrderService.Interfaces;

namespace OrderTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login()
        {
            var token = await _authService.LoginAsync();
            return Ok(token);
        }
    }
}
