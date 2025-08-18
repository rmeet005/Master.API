using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.Model
{
    public class Users
    {
            [Key]
            public int Uid { get; set; }
            public string? UserName { get; set; }
            public String? Email { get; set; }
            public string? PasswordHash { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedAt { get; set; }
            public bool IsDeleted { get; set; } = false;

            public List<UserRoles> userroles { get; set; }

        }
    }
