﻿using Application.Chapitres.Queries.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chapitres.Queries
{
    public class ChapitresVM
    {
        public IList<ChapitresDto> Chapitres { get; set; }
    }
}
