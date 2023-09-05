using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Rating_Info.Queries
{

    public record GetRatingInfoQueries(int id) : IRequest<AlreadyRated>;

    public class GetRatingInfoQueriesHandler : IRequestHandler<GetRatingInfoQueries, AlreadyRated>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        public GetRatingInfoQueriesHandler(IApplicationDbContext context,IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<AlreadyRated> Handle(GetRatingInfoQueries request, CancellationToken cancellationToken)
        {
            var user_id=_user.getUserId();
            var rates = await _context.Ratings
                .Where(t => t.storyTellId == request.id)
                .ToListAsync()??throw new NotFoundException();
            var rate_info = new AlreadyRated()
            {
                totalRate = (double) rate_value(rates)
            };
            var rate = rates.SingleOrDefault(t => t.user_id == user_id);
            if (rate == null) { rate_info.Rated = 0;rate_info.alreadyRated = false; }
            else { rate_info.Rated = rate.note;rate_info.alreadyRated = true; }

            return rate_info;

        }

        private double rate_value(List<RatingInfos> ratings)
        {
            var value = 0;
            var effectif = ratings.Count();
            if (effectif == 0) effectif++;
            foreach (var rating in ratings)
            {
                value += rating.note;
            }
            var rate = (double) value / effectif;
            return Math.Round(rate,2);
        }
    }
}
