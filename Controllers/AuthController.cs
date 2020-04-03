using System.Threading.Tasks;
using advancedwebapi.Auth;
using advancedwebapi.DTOs;
using advancedwebapi.Models;
using advancedwebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace advancedwebapi.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDTO newUser)
        {

            ServiceResponse<int> response = await _authRepo.Register(new User { Username = newUser.Username }, newUser.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDTO request)
        {

            ServiceResponse<string> response = await _authRepo.Login(request.Username, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }
    }
}