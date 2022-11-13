using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    //new
    public class Idees
    {
        public int IdIdee { get; set; }
        public string Idea { get; set; }
        public int IdStoryTelling { get; set; }
        public virtual StoryTelling StoryTelling { get; set;}
    }
}
