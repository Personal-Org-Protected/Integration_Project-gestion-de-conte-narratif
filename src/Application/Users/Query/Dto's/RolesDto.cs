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
    public class RolesDto : IMapFrom<Roles>
    {
        public int IdRole { get; set; }
        public string RoleLibelle { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Roles, RolesDto>();
        }
    }
}
