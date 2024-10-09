using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public class UserCreate
    {
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string phone { get; set; }

    }
}
