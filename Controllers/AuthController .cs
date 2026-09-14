using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiForJWT.Services;

namespace WebApiForJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            //Geting Id, Name, Role by username and password from database

            if (username == "admin" && password == "1234")
            {
                var token = _jwtService.GenerateToken(1, "admin", "Admin");  //Sending Id, Name, Role

                return Ok(new { token = token });
            }

            return Unauthorized(new
            {
                message = "Invalid username or password"
            });
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUsers()
        {
            return Ok(new
            {
                message = "You are authorized to access this API.",
                username = User.Identity?.Name
            });
        }
    }
}

