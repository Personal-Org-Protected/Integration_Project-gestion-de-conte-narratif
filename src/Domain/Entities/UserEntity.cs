using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    //new
    public class UserEntity
    {
        public string IdUser { get; set; }
        public string user_id { get; set; }
        public virtual User user { get; set; }
        public virtual Library Library { get; set; }
        public ICollection<Forfait_UserIntern> ForfaitUser { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }//new
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<StoryTelling> Stories { get; set; }
    }
}
