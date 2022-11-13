using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StoryTelling
    {

        public int IdStoryTelling { get; set; }
        public string? url { get; set; }//new
        public string NameStory { get; set; }
        public string IdUser { get; set; }
        public double price { get; set; }
        public string Sypnopsis { get; set; }
        public int? idTag { get; set; }
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd -- H:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateCreation { get; set; }
        public virtual UserEntity User { get; set; }
        public int IdZone { get; set; }//new
        public virtual Tag Tags { get; set; }
        public virtual ZoneCommentary ZoneCommentary { get; set; }//new
        public virtual ICollection<StoryTellBox> StoryTellBox { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Idees> Idees { get; set; }
        //public virtual ICollection<Commentaires> Commentaires { get; set; }//new
        public virtual ICollection<Chapitre> Chapitres { get; set; }
 
    }
}
