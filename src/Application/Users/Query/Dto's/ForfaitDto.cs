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
    public class ForfaitDto : IMapFrom<ForfaitClient>
    {
        public int IdForfait { get; set; }
        public string ForfaitLibelle { get; set; }
        public bool IsForAuthor { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ForfaitClient, ForfaitDto>();
        }
    }
}
