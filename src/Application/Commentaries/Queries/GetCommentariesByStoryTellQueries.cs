using Application.Commentaries.Queries.Dto;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commentaries.Queries
{
    public record GetCommentariesByStoryTellQueries(int idZone, int pgNumber) : IRequest<PaginatedItems<CommentaryDto>>;


    public class GetCommentariesByStoryTellQueriesHandler : IRequestHandler<GetCommentariesByStoryTellQueries, PaginatedItems<CommentaryDto>>
    {

        private const int _pageSize = 10;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCommentariesByStoryTellQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedItems<CommentaryDto>> Handle(GetCommentariesByStoryTellQueries request, CancellationToken cancellationToken)
        {
            return await _context.Commentaries
               .Where(d => d.IdZone == request.idZone && d.signal<10)
               .ProjectTo<CommentaryDto>(_mapper.ConfigurationProvider)
               .OrderBy(t => t.DateCreation)//mettre date => rajouter migration
               .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no commentary available");

        }
    }
}
