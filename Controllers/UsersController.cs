using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Interfaces.Services;
using WebApi.Models.Request;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class UsersController : ControllerBase
    {
        
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticateRequest model)
        {
            var response = await _userService.AuthenticateAsync(model).ConfigureAwait(false);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

 
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest model)
        {
            var response = await _userService.Register(model).ConfigureAwait(false);

            if (response == null)
                return BadRequest(new { message = "Registration error" });

            return Ok(response);
        }
    }
}
