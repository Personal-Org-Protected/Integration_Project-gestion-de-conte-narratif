using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.StoryTellings.Queries.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryTellings.Queries
{
    public record GetStoryByUserQueries(int pgNumber,int idTag) : IRequest<PaginatedItems<StoryTellingDto>>;

    public class GetStoryByUserQueriesHandler : IRequestHandler<GetStoryByUserQueries, PaginatedItems<StoryTellingDto>>
    {
        private const int _pageSize = 3;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public GetStoryByUserQueriesHandler(IApplicationDbContext context, IMapper mapper,IUser user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }

        public async Task<PaginatedItems<StoryTellingDto>> Handle(GetStoryByUserQueries request, CancellationToken cancellationToken)
        {
            var user_id = _user.getUserId();
            var tag = await getDefaultTag(request.idTag);
            var histoire = await GetBooks(request, tag, user_id,cancellationToken);
            return histoire;
            //return await _context.StoryTellings
            //    .Where(d => d.user_id == user_id && d.idTag==tag)
            //    .ProjectTo<StoryTellingDto>(_mapper.ConfigurationProvider)
            //    .OrderBy(t => t.price)
            //    .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken);
        }

        private async Task<PaginatedItems<StoryTellingDto>> GetBooks(GetStoryByUserQueries request, int tag,string user_id, CancellationToken cancellationToken)
        {
            if (tag > await getDefaultTag(0))
            {
                return await getResearchTag(request, tag,user_id, cancellationToken);
            }
            var histoires = await _context.StoryTellings
                .Where(d=>d.user_id == user_id)
                .ProjectTo<StoryTellingDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.DateCreation)
                .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken);
            return histoires;
        }


        private async Task<PaginatedItems<StoryTellingDto>> getResearchTag(GetStoryByUserQueries request, int tag, string user_id,CancellationToken cancellationToken)
        {

            var researches = await _context.StoryTellings
                   .Where(d => d.user_id==user_id && d.idTag == tag) //changer ici
                   .ProjectTo<StoryTellingDto>(_mapper.ConfigurationProvider)
                   .OrderBy(t => t.DateCreation)
                   .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken);
            return researches;
        }

        private async Task<int> getDefaultTag(int idTag)
        {
            if (idTag == 0)
            {
                var result = await _context.Tag
                                   .Where(t => t.NameTag == "Default")
                                   .SingleOrDefaultAsync() ?? throw new NotFoundException("no tag by default found"); idTag = result.IdTag;
            }
            return idTag;
        }
    }
}
