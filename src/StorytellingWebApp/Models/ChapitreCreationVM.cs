namespace StorytellingWebApp.Models
{
    public class ChapitreCreationVM
    {
        public int idStoryTelling { get; set; }
        public int idImage { get; set; }
        public int idStory { get; set; }
        public Image image { get; set; }
        public Story story { get; set; }
        public Chapitre chapitre { get; set; }
        public List<Image> images { get; set; }
        public List<Story> stories { get; set; }
        public PaginatedItems<Image> PaginatedItems { get; set; }
        public PaginatedItems<Chapitre> PaginatedChaps { get; set; }
    }
}
