using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ZoneCommentary
    {
        public int IdZone { get; set; }
        public bool Activated { get; set; }
        public virtual StoryTelling StoryTelling { get; set;}

        public virtual ICollection<Commentaires> Commentaires { get; set; }

    }
}
