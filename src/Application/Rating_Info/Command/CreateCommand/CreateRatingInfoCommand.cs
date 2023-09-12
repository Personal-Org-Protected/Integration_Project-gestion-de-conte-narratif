using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Rating_Info.Command.CreateCommand
{
    public record CreateRatingInfoCommand(int id, int rate) : IRequest<Result>;


    public class CreateRatingInfoCommandHandler : IRequestHandler<CreateRatingInfoCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public CreateRatingInfoCommandHandler(IApplicationDbContext context, IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<Result> Handle(CreateRatingInfoCommand request, CancellationToken cancellationToken)
        {

            var user_id = _user.getUserId();
            var rateConf = new RatingInfos()
            {
                storyTellId = request.id,
                user_id = user_id,
                note = request.rate
            };
            await _context.Ratings.AddAsync(rateConf);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) return Result.Success("StoryTellings updated with success");
            return Result.Failure("Action Failed : StoryTellings could not be updated", new List<string>());
        }
    }
}
