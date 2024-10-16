﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ideas.Queries
{
    public record GetIdeaByStoryTellIdQueries(int idStoryTell) : IRequest<IdeaVM>;

    public class GetIdeaByStoryTellIdQueriesHandler : IRequestHandler<GetIdeaByStoryTellIdQueries, IdeaVM>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetIdeaByStoryTellIdQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IdeaVM> Handle(GetIdeaByStoryTellIdQueries request, CancellationToken cancellationToken)
        {
            var Idea = await _context.Ideas
               .Where(t => t.IdStoryTelling == request.idStoryTell)
               .ToListAsync();
           var Ideas = _mapper.Map<List<IdeaDto>>(Idea);
            return new IdeaVM()
            {
                Ideas =  Ideas
            };
        }
    }
}
