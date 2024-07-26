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
        public string user_id { get; set; }
        public int? like { get; set; }
        public int signal { get; set; }
        public string Commentaire { get; set; }
        public int IdZone { get; set; }//new
        public DateTime DateCreation { get;set; }
        public virtual ZoneCommentary ZoneCommentary { get; set; }//new
    }
}
