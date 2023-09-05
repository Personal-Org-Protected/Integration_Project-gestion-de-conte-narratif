using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notification
    {
        public int idNotification { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public bool read { get; set; }
        public DateTime created { get; set; }
        public string user_id { get; set; }
        public virtual User User { get; set; }
    }
}
