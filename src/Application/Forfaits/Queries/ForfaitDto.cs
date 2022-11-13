using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Forfaits.Queries
{
    public class ForfaitDto : IMapFrom<ForfaitClient>
    {
        public int IdForfait { get; set; }
        public string ForfaitLibelle { get; set; }
        public double ForfaitValue { get; set; }
        public double Reduction { get; set; }
        public bool IsForAuthor { get; set;}
        public int IdRole { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ForfaitClient, ForfaitDto>();
        }
    }
}
