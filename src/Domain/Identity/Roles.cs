using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public class Roles
    {
        public int IdRole { get; set; }
        public string RoleLibelle { get; set; }
        public bool IsAdmin { get; set; }
        public string AuthRoleId { get; set; }
        public virtual ICollection<Roles_Users> UsersRoles { get; set; }//new
        public virtual ICollection<ForfaitClient> ForfaitClients { get; set; }//new
    }
}
