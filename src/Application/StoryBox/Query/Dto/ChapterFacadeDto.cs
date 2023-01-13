using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryBox.Query.Dto
{
    public class ChapterFacadeDto : IMapFrom<Chapitre>
    {
        public int IdChapitre { get; set; }//new
        public int Order { get; set; }
        public int IdStory { get; set; }
        public StoryDto story { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Chapitre, ChapterFacadeDto>();
        }
    }
}
