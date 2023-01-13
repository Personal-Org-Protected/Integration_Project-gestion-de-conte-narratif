using Application.Commentaries.Queries.Dto;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
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
    public record GetCommentairesByUserQueries(string user_id):IRequest<CommentaryVM>;

    public class GetCommentairesByUserQueriesHandler : IRequestHandler<GetCommentairesByUserQueries, CommentaryVM>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCommentairesByUserQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CommentaryVM> Handle(GetCommentairesByUserQueries request, CancellationToken cancellationToken)
        {
            var commentaires=await _context.Commentaries
               .Where(d => d.user_id == request.user_id)
               .ProjectTo<CommentaryDto>(_mapper.ConfigurationProvider)
               .OrderBy(t => t.DateCreation).ToListAsync() ?? throw new NotFoundException("there is no commentary available");
            return new CommentaryVM()
            {
                commentaries = commentaires,
            };
        }
    }

}
