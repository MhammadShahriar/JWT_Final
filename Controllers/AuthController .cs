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
            if (username == "admin" && password == "1234")
            {
                var token = _jwtService.GenerateToken(
                    1,
                    "admin",
                    "Admin");

                return Ok(new
                {
                    token = token
                });
            }

            return Unauthorized(new
            {
                message = "Invalid username or password"
            });
        }
    }
}

