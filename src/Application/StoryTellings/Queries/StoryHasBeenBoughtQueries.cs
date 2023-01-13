using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.StoryTellings.Queries.Dto;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryTellings.Queries
{
    public record StoryHasBeenBoughtQueries(string user_id):IRequest<HasBeenBoughtDto>;

    public class StoryHasBeenBoughtQueriesHandler:IRequestHandler<StoryHasBeenBoughtQueries, HasBeenBoughtDto>
    {
        private readonly IApplicationDbContext _context;

        public StoryHasBeenBoughtQueriesHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HasBeenBoughtDto> Handle(StoryHasBeenBoughtQueries request, CancellationToken cancellationToken)
        {
            var stories = await _context.StoryTellings
                .Where(t=>t.user_id ==request.user_id)
                .ToListAsync()?? throw new NotFoundException();

            return check(stories);
        }

        private HasBeenBoughtDto check(List<StoryTelling> storyTellings)
        {
            var result = new HasBeenBoughtDto();
            foreach (var storyTelling in storyTellings)
            {
                if (storyTelling.numberRef > 0) { result.IsBought = true; return result; }
            }
            return result;
        }
    }
}
