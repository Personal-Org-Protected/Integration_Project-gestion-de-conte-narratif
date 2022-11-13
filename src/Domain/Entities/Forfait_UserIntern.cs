using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Forfait_UserIntern
    {

        public string IdUser { get; set; }
        public int IdForfait { get; set; }
        public ForfaitClient ForfaitClient { get; set; }
        public UserEntity User { get; set; }
    }
}
