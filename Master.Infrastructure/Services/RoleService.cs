using Master.Application.Repository;
using Master.Domain.Model;
using Master.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Infrastructure.Services
{
    public class RoleService:IRoleService
    {
        ApplicationDbContext db;
        public RoleService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Roles> FetchAllRoles()
        {
            var data = db.Roles.Where(x => x.IsDeleted == false).ToList();
            return data;

        }
    }
}
