using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Chapitre
    {
        public int IdChapitre { get; set; }
        public int IdImage { get; set; }
        public int IdStory { get; set; }
        public int IdStoryTelling { get; set; }

        public int Order { get; set; }
        public virtual StoryTelling StoryTelling { get; set; }
        public virtual Image Image { get; set; }
        public virtual Story Story { get; set; }
    }
}
