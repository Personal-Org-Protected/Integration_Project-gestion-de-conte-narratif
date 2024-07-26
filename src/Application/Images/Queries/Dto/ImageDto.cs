using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Images.Queries.Dto
{
    public class ImageDto : IMapFrom<Image>
    {
        public int IdImage { get; set; }
        public string NomImage { get; set; }
        public string descriptionImage { get; set; }
        public string PathImage { get; set; }
        public string Uri { get; set; }
        public int IdTag { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateModif { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Image, ImageDto>();
        }
    }
}
