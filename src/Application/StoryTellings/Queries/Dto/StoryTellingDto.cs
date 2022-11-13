using Application.Chapitres.Queries;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryTellings.Queries.Dto
{
    public class StoryTellingDto : IMapFrom<StoryTelling>
    {
        public int IdStoryTelling { get; set; }
        public string NameStory { get; set; }
        public double price { get; set; }
        public string url { get; set; }
        public int? idTag { get; set; }
        public int IdZone { get; set; }
        //public ICollection<ChapitresDto> Chapitres { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoryTelling, StoryTellingDto>();
        }
    }
}
