using Master.Domain.DTO;
using Master.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.Text;

namespace Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ApplicationDbContext db;
        IConfiguration configuration;
        public LoginController(ApplicationDbContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult login([FromBody] LoginDto U)
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == U.UserName && x.PasswordHash == U.PasswordHash);
            var role = db.UserRoles.Include(x=>x.Roles).ToList();
            var rolename=role.FirstOrDefault(x => x.UserId == user.Uid)?.Roles?.RoleName;
            if (user != null)
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserName", user.UserName),
                new Claim("Role",rolename)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signIn
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { token = tokenString, username = user.UserName,role=rolename });
            }

            return Unauthorized("Invalid credentials");
        }
    }
}
