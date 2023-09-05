using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Basket
    {
        public string basket_id { get; set; }
        public string user_id { get; set; }
        public bool isEmpty { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<BasketItems> Items { get; set; }

    }
}
