using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Library
    {
        public string IdLibrary { get; set; }
        public string NameLibrary { get; set; }
        public string user_id { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<StoryTellBox> StoryTellBoxes { get; set; }
    }
}
