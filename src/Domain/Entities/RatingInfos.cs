using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RatingInfos
    {
        public string user_id { get; set; }
        public int storyTellId { get; set; }
        public int note { get; set; }
        public User User { get; set; }
        public StoryTelling StoryTell { get; set; }
    }
}
