using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repleet.Contracts;
using Repleet.Data;
using Repleet.Models.Entities;
using Repleet.Services;
using System.Security.Claims;

namespace Repleet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        
        private readonly ILogger<ProblemsAPIController> _logger;


        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> Register( ApplicationUserDTO request)
        {

            ApplicationUser user = await authService.RegisterAsync(request);
            if (user == null) { return BadRequest("username already Exists"); }

            return Ok(user);

        }

        [HttpPost("login")]

        public async Task<ActionResult<TokenResponseDTO>> Login( ApplicationUserDTO request)
        {

            TokenResponseDTO response = await authService.LoginAsync(request);
            if (response == null) { return BadRequest("Invalid Username or Password"); }

            return Ok(response);
        }

        [Authorize]
        [HttpGet("pingauth")]
        
        public IActionResult PingAuth()
        {
            return Ok("You Are Authenticated!");
        }


        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDTO>> RefreshToken(RefreshTokenRequestDTO request) {

            
            var result = await authService.RefreshTokensAsync(request);

            if (result is null || result.RefreshToken == null || result.AccessToken == null) {
                return Unauthorized("Invalid Refresh Token");
            }

            return Ok(result);

        }

    }
}
