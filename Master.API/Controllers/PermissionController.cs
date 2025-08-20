using Master.Application.Repository;
using Master.Domain.DTO;
using Master.Domain.Model;
using Master.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        ApplicationDbContext db;
        IDashboardervice service;
        public PermissionController(ApplicationDbContext db,IDashboardervice service)
        {
            this.db = db;
            this.service = service;
        }

        [HttpPost("AssignPermissions")]
        public IActionResult AssignPermissions([FromBody] AssignPermissionDTO dto)
        {
            service.AddPermission(dto);
            return Ok(new { message = "Permission assigned successfully" });
        }

        [HttpGet("GetUsersByRole/{roleId}")]
        public IActionResult GetUsers(int roleId)
        {
            var data = (from u in db.Users
                        join ur in db.UserRoles on u.Uid equals ur.UserId
                        where ur.RoleId == roleId
                        select u).ToList();

            if (data == null || data.Count == 0)
                return NotFound("No users found for this role");

            return Ok(data);
        }

        [HttpGet("GetPermissionsByUser/{userName}")]
        public IActionResult GetPermissionsByUser(string userName)
        {
            var permissions = db.Permission
                .Where(u => u.UserName == userName)
                .Select(u => u.PermissionName)
                .ToList();

            if (permissions == null || permissions.Count == 0)
                return NotFound("No permissions found for this user");

            return Ok(permissions);
        }
    }
}
