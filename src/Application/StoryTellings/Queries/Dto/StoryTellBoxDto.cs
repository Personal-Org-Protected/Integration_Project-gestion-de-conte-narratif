using Application.Common.Mappings;
using Application.StoryTellings.Queries.Dto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryTellings.Queries.Dto
{
    public class StoryTellBoxDto : IMapFrom<StoryTellBox>
    {
        public int IdBox { get; set; }//new
        public int lastPageChecked { get; set; }//new
        public int IdStoryTell { get; set; }//new
        public ICollection<ChapitreDto> chapitres { get; set; }//new
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoryTellBox, StoryTellBoxDto>();
        }
    }
}
