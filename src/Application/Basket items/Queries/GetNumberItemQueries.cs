using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Basket_items.Queries
{

    public record GetNumberItemQueries() : IRequest<int>;

    public class GetNumberItemQueriesHandler : IRequestHandler<GetNumberItemQueries, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public GetNumberItemQueriesHandler(IApplicationDbContext context, IUser user)
        {
            _context = context;
            _user = user;
         }
        public async Task<int> Handle(GetNumberItemQueries request, CancellationToken cancellationToken)
        {

            var user_id = _user.getUserId();
            var basket = await _context.Basket
                .Where(t => t.user_id == user_id)
                .SingleOrDefaultAsync() ?? throw new NotFoundException();
            var entity = await _context.Basket_items
                .Where(t => t.basket_id == basket.basket_id)
                .ToListAsync();
            return  entity.Count;
        }

    }
}
