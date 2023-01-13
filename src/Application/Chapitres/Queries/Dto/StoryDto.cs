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
    public class StoryDto : IMapFrom<Story>
    {
        public int IdStory { get; set; }
        public string NomStory { get; set; }
        public string TextStory { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateModif { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Story, StoryDto>();
        }
    }
}
