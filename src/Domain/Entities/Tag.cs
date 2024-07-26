using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tag
    {
        public int IdTag { get; set; }
        public string NameTag { get; set; }
        public double numberRef { get; set; }
        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<StoryTelling> StoryTellings { get; set; }
    }
}
