using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ideas.Queries
{
    public class IdeaDto : IMapFrom<Idees>
    {
        public string Idea { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Idees, IdeaDto>();
        }
    }
}
