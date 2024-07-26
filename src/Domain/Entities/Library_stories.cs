using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Library_stories
    {
        public string IdLibrary { get; set; }
        public virtual Library Library { get; set; }
        public int IdStoryTellBox { get; set; }
       
        public virtual StoryTellBox StoryTellBox { get; set;}
    }
}
