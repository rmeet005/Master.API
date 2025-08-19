using Master.Application.Repository;
using Master.Domain.DTO;
using Master.Domain.Model;
using Master.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ApplicationDbContext db;
        ILoginService Service;
        IConfiguration configuration;
        public LoginController(ApplicationDbContext db, IConfiguration configuration, ILoginService Service)
        {
            this.db = db;
            this.configuration = configuration;
            this.Service = Service;
        }

        
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto login)
        {
            if (login == null || string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.PasswordHash))
            {
                return BadRequest("Invalid login request.");
            }
            try
            {
                 var result=Service.Login(login);
                return Ok(new
                {
                    username = result.UserName,
                    role = result.RoleName,
                    token = result.Token
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
