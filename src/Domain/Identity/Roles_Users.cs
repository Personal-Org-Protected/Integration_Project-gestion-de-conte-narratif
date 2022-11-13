using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public class Roles_Users
    {
        public string user_id { get; set; }
        public int idRole { get; set;}
        public User User { get; set; }
        public Roles Roles { get; set; }
    }
}
