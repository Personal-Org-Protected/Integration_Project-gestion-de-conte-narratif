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
        public string IdUser { get; set; }
        public string url { get; set; }
        public string Sypnopsis { get; set; }
        public int? idTag { get; set; }
        public int IdZone { get; set; }
        public int numberRef { get; set; }
        public bool Finished { get; set; }
        public DateTime DateCreation { get; set; }

        public double? rating { get; set; }//nouveau

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoryTelling, StoryTellingDto>();
        }
    }
}
