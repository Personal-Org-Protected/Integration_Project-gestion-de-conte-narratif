using System.ComponentModel.DataAnnotations;

namespace StorytellingWebApp.Models
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
    }
}
