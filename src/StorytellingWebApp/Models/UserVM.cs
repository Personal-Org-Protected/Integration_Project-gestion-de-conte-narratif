namespace StorytellingWebApp.Models
{
    public class UserVM
    {
        public User user { get; set; }

        public PaginatedItems<User> PaginatedItems { get; set; }
    }
}
