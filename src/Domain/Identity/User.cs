using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Identity
{
    public class User
    {
        public string user_id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string? avatar { get; set; }//new  
        public string b2cObjId { get; set; }
        public string userRole { get; set; }// a voir

        public virtual Library Library { get; set; }
        public virtual Basket Basket { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<RatingInfos> Ratings { get; set; }//new
        public virtual ICollection<Transaction> Transactions { get; set; }//new
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<StoryTelling> Stories { get; set; }
    }


}
