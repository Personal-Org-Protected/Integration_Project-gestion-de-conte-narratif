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
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string phoneNumber { get; set; }
       
        public UserEntity UserEntity { get; set; }//new
        public ICollection<Roles_Users> UsersRoles { get; set; }//new
    }
}
