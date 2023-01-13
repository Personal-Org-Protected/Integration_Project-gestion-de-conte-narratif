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
    public record AlreadyHaveForfaitLambdaQueries(string user_id) : IRequest<HaveForfaitDto>;


    public class AlreadyHaveForfeitLambdaQueriesHandler : IRequestHandler<AlreadyHaveForfaitLambdaQueries, HaveForfaitDto>
    {
        private readonly int idDefaultForfait = 1;

        private readonly IApplicationDbContext _context;

        public AlreadyHaveForfeitLambdaQueriesHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HaveForfaitDto> Handle(AlreadyHaveForfaitLambdaQueries request, CancellationToken cancellationToken)
        {
            var user_forfaits = await _context.Forfait_Users
                .Where(t => t.user_id ==request.user_id/* && t.IdForfait!= idDefaultForfait*/)
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
