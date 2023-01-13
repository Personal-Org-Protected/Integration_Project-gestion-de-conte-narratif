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
    public record AlreadyHaveForfaitAuthorQueries(string user_id):IRequest<HaveForfaitDto>;

    public class AlreadyHaveForfeitAuthorQueriessHandler : IRequestHandler<AlreadyHaveForfaitAuthorQueries, HaveForfaitDto>
    {

        private readonly IApplicationDbContext _context;

        public AlreadyHaveForfeitAuthorQueriessHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HaveForfaitDto> Handle(AlreadyHaveForfaitAuthorQueries request, CancellationToken cancellationToken)
        {
            var user_forfaits = await _context.Forfait_Users
                .Where(t => t.user_id == request.user_id)
                .ToListAsync();//a enlever la verification sur les auteurs
            return await checkAuthorForfait(user_forfaits);
        }

        private async Task<HaveForfaitDto> checkAuthorForfait(List<Forfait_UserIntern> user_forfaits)
        {
            var response = new HaveForfaitDto();
            foreach (var forfait in user_forfaits)
            {

                if (await forAuthor(forfait.IdForfait))
                {
                    response.HaveForfait = true;
                    response.CurrentForfait = forfait.IdForfait;
                    return response;
                }
            }
            response.HaveForfait = false;
            return response;
        }
        private async Task<bool> forAuthor(int id)
        {
            var result = await _context.Forfaits.FindAsync(id);
            if (result.IsForAuthor) return true;
            return false;
        }
    }
}
