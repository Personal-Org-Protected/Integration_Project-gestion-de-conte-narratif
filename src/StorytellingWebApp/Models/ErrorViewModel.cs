namespace StorytellingWebApp.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public string msg { get; set; }
        public string redirectTo { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}