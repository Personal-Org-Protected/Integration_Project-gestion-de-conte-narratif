using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Libraries.Queries
{
    public class LibrariesDto : IMapFrom<Library>
    {
        public string IdLibrary { get; set; }
        public string NameLibrary { get; set; }
        public string user_id { get; set; }

        //public ICollection<StoryTellBoxDto> StoryTellBoxDtos { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Library, LibrariesDto>();
        }
    }
}
