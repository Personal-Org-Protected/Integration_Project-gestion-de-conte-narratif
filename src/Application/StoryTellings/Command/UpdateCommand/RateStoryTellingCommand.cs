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

namespace Application.StoryTellings.Command.UpdateCommand
{
    public record RateStoryTellingCommand (int id): IRequest<Result>;


    public class RateStoryTellingCommandHandler : IRequestHandler<RateStoryTellingCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public RateStoryTellingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(RateStoryTellingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.StoryTellings
           .FindAsync(new object[] { request.id }, cancellationToken) ?? throw new NotFoundException("StoryTell", request.id);

            var rateConfig = await _context.Ratings
                .Where(t=>t.storyTellId==request.id)
                .ToListAsync() ?? throw new NotFoundException("Rates", request.id);

            entity.rating = rate_value(rateConfig);
            _context.StoryTellings.Update(entity);

            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) return Result.Success("StoryTellings updated with success");
            return Result.Failure("Action Failed : StoryTellings could not be updated", new List<string>());
        }

        private double rate_value(List<RatingInfos> ratings)
        {
            var value = 0;
            var effectif=ratings.Count();
            foreach (var rating in ratings)
            {
                value += rating.note;
            }
            var rate= (double) value / effectif;
            return  Math.Round(rate, 2); 
        }
    }
}
