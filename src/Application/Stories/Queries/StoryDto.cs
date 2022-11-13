using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stories.Queries
{
    public class StoryDto : IMapFrom<Story>
    {

        public int IdStory { get; set; }
        public string NomStory { get; set; }
        public string TextStory { get; set; }
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateCreation { get; set; }
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateModif { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Story, StoryDto>();
        }
    }
}
