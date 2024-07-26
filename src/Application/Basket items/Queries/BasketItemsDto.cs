using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Basket_items.Queries
{
    public class BasketItemsDto: IMapFrom<BasketItems>
    {
        public int IdStoryTelling { get; set; }
        public void Mapping(Profile profile)
    {
        profile.CreateMap<BasketItems, BasketItemsDto>();
    } 
}
}

