﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public class DeleteRoleUser
    {
        public ICollection<string> roles { get; set; }
    }
}
