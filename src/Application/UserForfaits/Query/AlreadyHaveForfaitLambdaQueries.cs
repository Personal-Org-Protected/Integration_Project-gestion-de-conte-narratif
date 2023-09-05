using Application.Common.Interfaces;
using Application.UserForfaits.Query.Dto;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserForfaits.Query
{
    public record AlreadyHaveForfaitLambdaQueries() : IRequest<HaveForfaitDto>;


    public class AlreadyHaveForfeitLambdaQueriesHandler : IRequestHandler<AlreadyHaveForfaitLambdaQueries, HaveForfaitDto>
    {
        private readonly int idDefaultForfait = 1;

        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        public AlreadyHaveForfeitLambdaQueriesHandler(IApplicationDbContext context,IUser user)
        {
            _context = context;
            _user = user;
        }

        public async Task<HaveForfaitDto> Handle(AlreadyHaveForfaitLambdaQueries request, CancellationToken cancellationToken)
        {
            var user_id=_user.getUserId();
            var user_forfaits = await _context.Forfait_Users
                .Where(t => t.user_id ==user_id/* && t.IdForfait!= idDefaultForfait*/)
                .ToListAsync();

         return await checkLambdaForfait(user_forfaits);
        }

        private async Task<HaveForfaitDto> checkLambdaForfait(List<Forfait_UserIntern> user_forfaits)
        {
            var response = new HaveForfaitDto();
            foreach (var forfait in user_forfaits) { 

                if (await notForAuthor(forfait.IdForfait))
                {
                    response.HaveForfait = true;
                    response.CurrentForfait = forfait.IdForfait;
                    return response;
                }
            }
            response.HaveForfait=false;
            return response;
        }
        private async Task<bool> notForAuthor(int id)
        {
            var result = await _context.Forfaits.FindAsync(id);
            if (result.IsForAuthor) return false;
            return true;

        }


    }
}
