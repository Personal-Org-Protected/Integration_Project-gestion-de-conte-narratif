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

namespace Application.Stories.Queries
{
    public record GetStoriesByIdQueries(int id):IRequest<StoryDto>;

    public class GetStoriesByIdQueriesHandler : IRequestHandler<GetStoriesByIdQueries, StoryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStoriesByIdQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StoryDto> Handle(GetStoriesByIdQueries request, CancellationToken cancellationToken)
        {
           var Story=await  _context.Stories.Where(t => t.IdStory == request.id).FirstOrDefaultAsync() ?? throw new NotFoundException("there is no story available");
            return _mapper.Map<StoryDto>(Story);
        }
    }

}
