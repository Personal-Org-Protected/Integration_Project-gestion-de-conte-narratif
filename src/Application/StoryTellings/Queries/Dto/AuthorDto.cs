using Application.Common.Mappings;
using AutoMapper;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryTellings.Queries.Dto
{
    public class AuthorDto : IMapFrom<User>
    {
        public string username { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, AuthorDto>();
        }
    }
}
