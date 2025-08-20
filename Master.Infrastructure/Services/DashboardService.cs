using Master.Application.Repository;
using Master.Domain.DTO;
using Master.Domain.Model;
using Master.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Infrastructure.Services
{
    public class DashboardService: IDashboardervice
    {
        ApplicationDbContext db;
        public DashboardService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public List<Users> GetUsers(int roleId)
        {
            var data = (from u in db.Users
                        join ur in db.UserRoles on u.Uid equals ur.UserId
                        where ur.RoleId == roleId
                        select u)
           .ToList();
            return data;
        }
        public void AddPermission(AssignPermissionDTO dto)
        {

            foreach (var p in dto.Permissions)
            {
                var userPerm = new Master.Domain.Model.Permission
                {
                 
                    UserName = dto.UserName,
                    RoleId = dto.RoleId,
                    PermissionName = p
                };
                db.Permission.Add(userPerm);
            }

            db.SaveChanges();
        }
        public List<string> GetPermissionsByUser(string UserName)
        {
            var permissions = db.Permission.Where(u => u.UserName == UserName).Select(u => u.PermissionName).ToList();
            return permissions;
        }
    }
}
