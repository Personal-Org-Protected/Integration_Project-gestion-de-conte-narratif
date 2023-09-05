using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Rating_Info.Command.UpdateCommand
{
    public record UpdateRatingInfoCommand(int id,int rate) : IRequest<Result>;


    public class UpdateRatingInfoCommandHandler : IRequestHandler<UpdateRatingInfoCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public UpdateRatingInfoCommandHandler(IApplicationDbContext context, IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<Result> Handle(UpdateRatingInfoCommand request, CancellationToken cancellationToken)
        {

            var user_id = _user.getUserId();
            var entity=await _context.Ratings
                .Where(t=>t.user_id == user_id && t.storyTellId==request.id)
                .SingleOrDefaultAsync() ?? throw new NotFoundException("Rate", request.id);

            entity.note = request.rate;
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) return Result.Success("StoryTellings updated with success");
            return Result.Failure("Action Failed : StoryTellings could not be updated", new List<string>());
        }

    }
}
