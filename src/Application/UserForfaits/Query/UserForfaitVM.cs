using Application.UserForfaits.Query.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserForfaits.Query
{
    public class UserForfaitVM
    {

        public ICollection<UserForfaitDto> UserForfaits { get; set; }
    }
}
