using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Interfaces.Services;
using WebApi.Models.Request;
using WebApi.Models.Response;
using WebApi.Models.UserModel;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class UsersController : ControllerBase
    {
        private List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "User", Password = "test" }
        };
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.AuthenticateAsync(model);

            if (response.Result == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response.Result);
        }

 
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            var response = _userService.Register(model);

            if (response.Result == null)
                return BadRequest(new { message = "Registration error" });

            return Ok(response.Result);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
           
            return Ok(_users);
        }
    }
}
