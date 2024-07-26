using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tags.Query
{
    public class TagDto : IMapFrom<Tag>
    {
        public int IdTag { get; set; }
        public string NameTag { get; set; }
        public double NumberRef { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Tag, TagDto>();
        }
    }
}
