using Master.Application.Repository;
using Master.Domain.DTO;
using Master.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Master.Infrastructure.Services
{
    public class LoginService : ILoginService
    {
        ApplicationDbContext db;
        IConfiguration configuration;
        public LoginService(ApplicationDbContext db, IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }
        public LoginResponseDto Login(LoginDto login)
        {
            var user = db.Users.FirstOrDefault(x => x.UserName == login.UserName && x.PasswordHash == login.PasswordHash);
            var role = db.UserRoles.Include(x => x.Roles).ToList();
            var rolename = role.FirstOrDefault(x => x.UserId == user.Uid)?.Roles?.RoleName;
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
                return new LoginResponseDto
                {
                    UserName = user.UserName,
                    RoleName = rolename,
                    Token = tokenString
                };
            }
            return new LoginResponseDto
            {
                UserName = null,
                RoleName = null,
                Token = null
            };
        }
    }
}
