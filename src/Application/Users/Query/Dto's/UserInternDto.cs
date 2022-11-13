using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Query.Dto_s
{
    public  class UserInternDto : IMapFrom<UserEntity>
    {

        public string IdUser { get; set; }
        public string user_id { get; set; }
        public ICollection<ForfaitDto> forfaitDtos { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserEntity, UserInternDto>();
        }
    }
}
