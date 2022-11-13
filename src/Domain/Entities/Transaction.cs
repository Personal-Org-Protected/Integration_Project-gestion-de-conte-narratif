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
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd -- H:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateTransaction { get; set; }
        //public string idLibrary { get; set; }
        //public virtual Library Library { get; set; }
        public int StoryTellId { get; set; }
        public virtual StoryTelling StoryTelling { get; set; }
        public string User_id { get; set; }
        public virtual UserEntity User { get; set; }

    }
}
