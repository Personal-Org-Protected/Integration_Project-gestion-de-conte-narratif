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
        public string password { get; set; }
        public string Location { get; set; }
        public DateTime BirthDate { get; set; }
        public string phoneNumber { get; set; }
        public string? description { get; set; }//new  
        public virtual Library Library { get; set; }
        public ICollection<Roles_Users> UsersRoles { get; set; }//new
        public virtual ICollection<Forfait_UserIntern> ForfaitUser { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }//new
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<StoryTelling> Stories { get; set; }
    }

    /*{
  "user_id": "admin_46634467-be58-45b1-9aa3-e9b42897fa17",
  "email": "adminuser@gmail.com",
  "username": "admin_user",
  "password": "admin_user98*",
  "location": "Bruxelles",
  "birthDate": "1998-12-18",
  "phoneNumber": "0486256235",
  "description": "The admin server"
}*/
}
