using Microsoft.AspNetCore.Mvc.Rendering;

namespace StorytellingWebApp.Models
{
    public class ImageVM
    {
        public int IdTag { get; set; }
        public string Uri { get; set; }
        public Image image { get; set; }
        public SelectList selectListItems { get; set; }
        public PaginatedItems<Image> PaginatedItems { get; set; }
    }
}
