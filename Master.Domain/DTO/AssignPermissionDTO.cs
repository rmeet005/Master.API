using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.DTO
{
    public class AssignPermissionDTO
    {
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public List<string> Permissions { get; set; }
    }
}
