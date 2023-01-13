using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Story
    {
        public int IdStory { get; set; }
        public string NomStory { get; set; }
        public string TextStory { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateModif { get; set; }
        public virtual Chapitre Chapitre { get; set; }// need to change that
    }
}
