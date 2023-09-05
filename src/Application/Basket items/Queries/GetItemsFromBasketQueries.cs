using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Basket_items.Queries
{

    public record GetItemsFromBasketQueries(string basket_id,int pgNumber) : IRequest<PaginatedItems<BasketItemsDto>>;

    public class GetItemsFromBasketQueriesHandler : IRequestHandler<GetItemsFromBasketQueries, PaginatedItems<BasketItemsDto>>
    {
        private const int _pageSize = 3;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemsFromBasketQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedItems<BasketItemsDto>> Handle(GetItemsFromBasketQueries request, CancellationToken cancellationToken)
        {
            var entity = await _context.Basket_items
                .Where(t => t.basket_id == request.basket_id)
                .ProjectTo<BasketItemsDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no ITEMS available") ?? throw new NotFoundException("Items", request);
            return entity;
        }

    }
}
