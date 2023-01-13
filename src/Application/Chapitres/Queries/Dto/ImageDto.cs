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
    public class ImageDto : IMapFrom<Image>
    {
        public int IdImage { get; set; }
        public string NomImage { get; set; }
        public string descriptionImage { get; set; }
        public string Uri { get; set; }
        public string user_id { get; set; }
        public int IdTag { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Image, ImageDto>();
        }
    }
}
