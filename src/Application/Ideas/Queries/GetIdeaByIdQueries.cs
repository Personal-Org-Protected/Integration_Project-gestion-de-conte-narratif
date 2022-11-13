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

namespace Application.Ideas.Queries
{
    public record GetIdeaByIdQueries(int id) : IRequest<IdeaDto>;

    public class GetIdeaByIdQueriesHandler : IRequestHandler<GetIdeaByIdQueries, IdeaDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetIdeaByIdQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IdeaDto> Handle(GetIdeaByIdQueries request, CancellationToken cancellationToken)
        {
            var Idea = await _context.Ideas
                .Where(t => t.IdIdee == request.id)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Idea", request);
            return _mapper.Map<IdeaDto>(Idea);
        }
    }
}
