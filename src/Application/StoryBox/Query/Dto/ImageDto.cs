using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryBox.Query.Dto
{
    public class ImageDto : IMapFrom<Image>
    {
        public string NomImage { get; set; }
        public string descriptionImage { get; set; }
        public string Uri { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Image, ImageDto>();
        }
    }
}
