using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    //new
    public class ForfaitClient
    {
       public int IdForfait { get; set; }
       public string ForfaitLibelle { get; set; }
       public double ForfaitValue { get; set; }
       public bool IsForAuthor { get; set; }
       public double Reduction { get; set; }
       public int RoleId { get; set; }
       public virtual Roles Roles { get; set; }
       public ICollection<Forfait_UserIntern> ForfaitUser { get; set; }

    }
}
