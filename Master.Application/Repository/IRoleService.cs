using Master.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Application.Repository
{
    public interface IRoleService
    {
        public List<Roles> FetchAllRoles();
    }
}
