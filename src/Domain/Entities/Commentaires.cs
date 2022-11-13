using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    //new
    public class Commentaires
    {
        public int IdCommentaire { get; set; }
        public string Owner { get; set; }
        public int signal { get; set; }
        public string Commentaire { get; set; }
        public int IdZone { get; set; }//new
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd -- H:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateCreation { get;set; }
        public virtual ZoneCommentary ZoneCommentary { get; set; }//new
    }
}
