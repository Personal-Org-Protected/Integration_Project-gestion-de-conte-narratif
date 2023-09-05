using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Methods;
using Application.StoryTellings.Queries.Dto;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryTellings.Queries
{

    public record GetStoryTellByIdQueries(int idStoryTell) : IRequest<StoryTellingDto>;

    public class GetStoryTellByIdQueriesHandler : IRequestHandler<GetStoryTellByIdQueries, StoryTellingDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;


        public GetStoryTellByIdQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }

        public async Task<StoryTellingDto> Handle(GetStoryTellByIdQueries request, CancellationToken cancellationToken)
        {
            var StoryTell = await _context.StoryTellings
                .FindAsync(request.idStoryTell) ?? throw new NotFoundException("there is no story available");
            return _mapper.Map<StoryTellingDto>(StoryTell);
        }
    }
}
