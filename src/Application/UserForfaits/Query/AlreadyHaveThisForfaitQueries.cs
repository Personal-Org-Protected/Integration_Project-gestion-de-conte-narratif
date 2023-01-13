using Application.Common.Interfaces;
using Application.UserForfaits.Query.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserForfaits.Query
{
    public record AlreadyHaveThisForfaitQueries(string user_id,int idForfait) : IRequest<HaveForfaitDto>;

    public class AlreadyHaveThisForfeitQueriesHandler : IRequestHandler<AlreadyHaveThisForfaitQueries, HaveForfaitDto>
    {

        private readonly IApplicationDbContext _context;

        public AlreadyHaveThisForfeitQueriesHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HaveForfaitDto> Handle(AlreadyHaveThisForfaitQueries request, CancellationToken cancellationToken)
        {
            var result = new HaveForfaitDto();
            var user_forfait = await _context.Forfait_Users
                .Where(t => t.IdForfait == request.idForfait && t.user_id == request.user_id)
                .FirstOrDefaultAsync();
            if (user_forfait == null) { result.HaveForfait = false;return result; }

            result.HaveForfait = true;
            result.CurrentForfait = request.idForfait;
            return result;
        }
    }
}
