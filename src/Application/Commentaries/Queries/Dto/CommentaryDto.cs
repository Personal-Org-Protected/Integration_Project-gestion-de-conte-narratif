using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commentaries.Queries.Dto
{
    public class CommentaryDto : IMapFrom<Commentaires>
    {
        public int IdCommentaire { get; set; } 
        public string user_id { get; set; }
        public string Commentaire { get; set; }
        public int like { get; set; }
        public int signal { get; set; }
        public DateTime DateCreation { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Commentaires, CommentaryDto>();
        }
    }
}
