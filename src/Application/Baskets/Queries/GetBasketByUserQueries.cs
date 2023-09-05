using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Baskets.Queries
{
    public record GetBasketByUserQueries : IRequest<BasketDto>;

    public class GetLibraryByOwnerQueryHandler : IRequestHandler<GetBasketByUserQueries, BasketDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public GetLibraryByOwnerQueryHandler(IApplicationDbContext context, IMapper mapper, IUser user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }
        public async Task<BasketDto> Handle(GetBasketByUserQueries request, CancellationToken cancellationToken)
        {
            var user_id = _user.getUserId();
            var library = await _context.Basket
                .Where(t => t.user_id == user_id) //creer un moel qui recupere la librairie et les hitsoires a l' interieur
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Panier", request);

            return _mapper.Map<BasketDto>(library);
        }
    }
}
