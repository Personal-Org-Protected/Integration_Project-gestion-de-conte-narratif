using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public class Token
    {
         public string access_token { get; set; }
        public double expires_in { get; set; }
    }
}
