using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StoryTellBox
    {
        public int IdBox { get; set; }//new
        public int lastPageChecked { get; set; }//new
        public int IdStoryTell { get; set; }//new
        public string IdLibrary { get; set; }//new 
        public virtual StoryTelling StoryTelling { get; set; }//new
        public virtual Library StoryTellLibrary { get; set; }//new
    }
}
