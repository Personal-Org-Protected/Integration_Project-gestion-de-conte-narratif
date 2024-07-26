using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryTellings.Queries.Dto
{
    public class ChapitreDto
    {
        public int IdChapitre { get; set; }
        public string ChapitreName { get; set; }
        public string narration { get; set; }

        public string url { get; set; }
        public string path { get; set; }
        public string nomImage { get; set; }

        public string descriptionImage { get; set; }
        public  int Order { get; set; }
    }
}
