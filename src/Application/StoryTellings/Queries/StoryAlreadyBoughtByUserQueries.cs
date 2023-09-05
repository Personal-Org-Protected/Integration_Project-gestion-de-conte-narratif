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


    public record StoryAlreadyBoughtByUserQueries(int id) : IRequest<HasBeenBoughtDto>;

    public class StoryAlreadyBoughtByUserQueriesHandler : IRequestHandler<StoryAlreadyBoughtByUserQueries, HasBeenBoughtDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public StoryAlreadyBoughtByUserQueriesHandler(IApplicationDbContext context,IUser user)
        {
            _context = context;
            _user = user;
        }

        public async Task<HasBeenBoughtDto> Handle(StoryAlreadyBoughtByUserQueries request, CancellationToken cancellationToken)
        {
            var user_id=_user.getUserId();
            var library=await _context.Libraries.Where(t=>t.user_id == user_id).SingleOrDefaultAsync();
            var storyBox = await _context.storyTellBoxes.Where(t => t.IdStoryTell == request.id && t.IdLibrary == library.IdLibrary).FirstOrDefaultAsync();

            return check(storyBox);
        }

        private HasBeenBoughtDto check(StoryTellBox storyTelling)
        {
            var result = new HasBeenBoughtDto();

            if (storyTelling !=null) { result.IsBought = true; }
            else result.IsBought = false;
            return result;
        }
    }
}
