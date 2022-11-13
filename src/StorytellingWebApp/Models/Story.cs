namespace StorytellingWebApp.Models
{
    public class Story
    {
        public int IdStory { get; set; }
        public string NomStory { get; set; }//changer le nom
        public string TextStory { get; set; }
        public int idEntity { get; set; }
    }
}
