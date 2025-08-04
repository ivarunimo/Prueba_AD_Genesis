using ContextoDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Services;
using BCrypt.Net;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BancaContext _context;
        private readonly ILogger<LoginController> _logger;
        private readonly IConfiguration _configuration;

        public LoginController(BancaContext context, ILogger<LoginController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<string>> LoginProcess(Login login)
        {
            
            var user = await _context.Users.FirstOrDefaultAsync(u => u.username == login.username);
            if (user == null)
            {
                return Unauthorized();
            }
            try
            {
                var confirmPass = Encrypt.VerifyPassword(login.password, user.password);
                if (confirmPass)
                {
                    int expirationMinutes = int.Parse(_configuration["JWTConfigure:accessTokenExpiration"]);
                    string secretKey = _configuration["JWTConfigure:secretKey"];
                    string token = TokenGenerator.GenerateToken(login.username, secretKey, expirationMinutes);
                    LoginResponseModel response = new LoginResponseModel { expiration = expirationMinutes, token = token, username = login.username, user_id = user.Id, fullName = user.fullName };
                    return Ok(
                        response
                        );
                }

                return Unauthorized();

            }
            catch (SaltParseException ex)
            {
                _logger.LogError(ex, "Error en verificar contraseña, no esta correctamente el hash");
                return StatusCode(500);

            }
            catch (Exception ex)
            {
                return StatusCode(500,new { message = ex.Message });
            }
            
        }
    }


    public class Login
    {
        public required string username { get; set; }
        public required string password { get; set; }
    }
        
}
