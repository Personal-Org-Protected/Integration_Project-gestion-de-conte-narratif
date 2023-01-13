using Application.Common.Mappings;
using AutoMapper;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Query.Dto_s
{
    public class UserSimpleInfoDto : IMapFrom<User>
    {
        public string username { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserSimpleInfoDto>();
        }
    }
}
