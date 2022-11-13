using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Image
    {
        public int IdImage { get; set; }
        public string NomImage { get; set; }
        public string descriptionImage { get; set; }
        public string PathImage { get; set; }
        public string Uri { get; set; }
        public string owner { get; set; }
        public int IdTag { get; set; }

        public DateTime DateCreation { get; set; }
        public DateTime DateModif { get; set; }
        public virtual Tag Tags { get; set; }
        public virtual ICollection<Chapitre> Chapitres { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
