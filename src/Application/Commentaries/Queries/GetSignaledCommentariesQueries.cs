using Application.Commentaries.Queries.Dto;
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

namespace Application.Commentaries.Queries
{

        public record GetSignaledCommentariesQueries(int pgNumber) : IRequest<PaginatedItems<CommentaryDto>>;


        public class GetSignaledCommentariesQueriesHandler : IRequestHandler<GetSignaledCommentariesQueries, PaginatedItems<CommentaryDto>>
        {

            private const int _pageSize = 20;
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetSignaledCommentariesQueriesHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<PaginatedItems<CommentaryDto>> Handle(GetSignaledCommentariesQueries request, CancellationToken cancellationToken)
            {//rajouter info avec les commentaires
                return  await _context.Commentaries
                   .Where(d => d.signal>=10)
                   .ProjectTo<CommentaryDto>(_mapper.ConfigurationProvider)
                   .OrderBy(t => t.DateCreation)
                   .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no commentary available");

            }


    }
}
