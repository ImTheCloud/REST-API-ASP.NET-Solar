using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PROJETDOTNETQ5.Models;
using PROJETDOTNETQ5.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace PROJETDOTNETQ5.Controllers
{
    [ApiController]

    // Controller class for handling user authentication and token generation
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;

        // Dependency injection of IConfiguration and IUserService
        public AuthController(IConfiguration _configuration, IUserService _userService)
        {
            configuration = _configuration;
            userService = _userService;
        }

        // Endpoint for user login and token generation
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.Username) &&
                   !string.IsNullOrEmpty(user.Password))
                {
                    var loggedInUser = userService.Get(user);
                    if (loggedInUser is null) return NotFound("User not found");

                    Claim cc = new Claim("normal", "");
                    if (loggedInUser.Username.Equals("Eric_admin"))
                    {
                        cc = new Claim("Age", "22");
                    }

                    // User found => build the token
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Name, loggedInUser.Username),
                        new Claim(ClaimTypes.Email, loggedInUser.EmailAddress),
                        new Claim(ClaimTypes.GivenName, loggedInUser.GivenName),
                        new Claim(ClaimTypes.Surname, loggedInUser.Surname),
                        new Claim(ClaimTypes.Role, loggedInUser.Role),
                        cc,
                    };

                    var token = new JwtSecurityToken
                    (
                        issuer: configuration["Jwt:Issuer"],
                        audience: configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(1),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                            SecurityAlgorithms.HmacSha256)
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(new { token = tokenString });
                }
                return BadRequest("Invalid user credentials ");
            }
            catch (ArgumentException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}