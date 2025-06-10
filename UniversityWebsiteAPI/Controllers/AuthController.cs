using Microsoft.AspNetCore.Mvc;
using UserService.Application.UserDTO;
using UserService.Application.UserServices;
using UserService.Core.Entities;
using UserService.Infrastructure;

namespace UserService.Api.Controllers
{
    public class AuthController : Controller
    {

        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            try
            {
                await _userService.RegisterAsync(dto);
                return Ok("User registered successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
