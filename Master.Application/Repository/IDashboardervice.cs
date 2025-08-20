using Master.Domain.DTO;
using Master.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Application.Repository
{
    public interface IDashboardervice
    {
        void AddPermission(AssignPermissionDTO dto);
        List<string> GetPermissionsByUser(string UserName);
    }
}
