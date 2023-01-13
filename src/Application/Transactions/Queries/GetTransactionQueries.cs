﻿using Application.Common.Exceptions;
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

namespace Application.Transactions.Queries
{
    public record GetTransactionQueries(string user_id,int pgNumber):IRequest<PaginatedItems<TransactionsDto>>;


    public class GetTransactionQueriesHandler : IRequestHandler<GetTransactionQueries, PaginatedItems<TransactionsDto>>
    {
        private const int _pageSize = 50;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTransactionQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedItems<TransactionsDto>> Handle(GetTransactionQueries request, CancellationToken cancellationToken)
        {
  
            return await _context.Transactions
            .Where(t=>t.user_id==request.user_id)
            .ProjectTo<TransactionsDto>(_mapper.ConfigurationProvider)
             .OrderBy(t => t.DateTransaction)
              .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no StoryTelling available");
        }
    }


}
