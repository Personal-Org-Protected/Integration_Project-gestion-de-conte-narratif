using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chapitres.Queries.Dto
{
    public class ChapitreOrderDto : IMapFrom<Chapitre>
    {
        public int IdChapitre { get; set; }
        public int IdImage { get; set; }
        public int IdStory { get; set; }
        public int IdStoryTelling { get; set; }
        public int Order { get; set; }

        public bool hasPrevious { get; set; }
        public bool hasNext { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Chapitre, ChapitreOrderDto>();
        }
    }
}
