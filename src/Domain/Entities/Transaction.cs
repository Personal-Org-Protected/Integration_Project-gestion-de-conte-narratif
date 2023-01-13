using Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string NameBook { get; set; }
        public double price { get; set; }
        public DateTime DateTransaction { get; set; }
        //public string idLibrary { get; set; }
        //public virtual Library Library { get; set; }
        public int StoryTellId { get; set; }
        public virtual StoryTelling StoryTelling { get; set; }
        public string user_id { get; set; }
        public virtual User User { get; set; }

    }
}
