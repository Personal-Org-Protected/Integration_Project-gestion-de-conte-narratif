﻿using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryBox.Query.Dto
{
    public class FacadeDto : IMapFrom<StoryTelling>
    {
        public string NameStory { get; set; }
        public string url { get; set; }
        public string user_id { get; set; }
        public string Sypnopsis { get; set; }
        public int? idTag { get; set; }
        public int IdZone { get; set; }//new
        public AuthorDto author { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoryTelling, FacadeDto>();
        }
    }
}
