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
        public string IdUser { get; set; }
        public virtual UserEntity Owner { get; set; }
        public virtual ICollection<StoryTellBox> StoryTellBoxes { get; set; }
        public virtual ICollection<Transaction> TransactionLibrary { get; set; }
    }
}
