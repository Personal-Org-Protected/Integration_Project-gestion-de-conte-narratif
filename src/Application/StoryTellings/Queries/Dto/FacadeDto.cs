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
    public class FacadeDto : IMapFrom<StoryTelling>
    {
        public int IdStoryTelling { get; set; }
        public string NameStory { get; set; }
        public string url { get; set; }
        public string user_id { get; set; }
        public double price { get; set; }
        public double ForfaitPrice { get; set; }
        public string Sypnopsis { get; set; }
        public int? idTag { get; set; }
        public int IdZone { get; set; }//new
        public double? rating { get; set; }//nouveau
        public DateTime DateCreation { get; set; }
        public AuthorDto author { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoryTelling, FacadeDto>();
        }
    }
}
