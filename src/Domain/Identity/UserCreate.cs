using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public class UserCreate
    {
        public string user_id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string connection { get; set; }
    }
}
