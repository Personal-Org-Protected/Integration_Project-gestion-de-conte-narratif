using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BasketItems
    {
        public string basket_id { get; set; }
        public int IdStoryTelling { get; set; }
        public virtual StoryTelling StoryTelling { get; set; }//new
        public virtual Basket Basket { get; set; }//new
    }
}
