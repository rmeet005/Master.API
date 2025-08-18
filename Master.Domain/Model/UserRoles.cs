using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Domain.Model
{
    public class UserRoles
    {
        [Key]
        public int UserRoleId { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public Users Users { get; set; }

        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        public Roles Roles { get; set; }

        public DateTime AssignedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
