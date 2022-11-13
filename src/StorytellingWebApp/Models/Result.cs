namespace StorytellingWebApp.Models
{
    public class Result
    {
        public int idEntity { get; set; }
        public bool Succeeded { get; set; }
        public string Msg { get; set; }
        public string[] Errors { get; set; }

        public Result(int idEntity, bool succeeded, string msg, string[] errors)
        {
            this.idEntity = idEntity;
            this.Succeeded = succeeded;
            this.Msg = msg;
            this.Errors = errors;
        }
    }
}
