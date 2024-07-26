using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.StoryBox.Query.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryBox.Query
{

    public record GetChapitresFacadeQueries(int idStoryTell, int pgNumber) : IRequest<PaginatedItems<ChapterFacadeDto>>;

    public class GetChapitreFacadeQueriesHandler : IRequestHandler<GetChapitresFacadeQueries, PaginatedItems<ChapterFacadeDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly int _pageSize = 4;

        public GetChapitreFacadeQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<ChapterFacadeDto>> Handle(GetChapitresFacadeQueries request, CancellationToken cancellationToken)
        {
            var chapters = await _context.Chapitres
                  .Where(t => t.IdStoryTelling == request.idStoryTell)
                   .ProjectTo<ChapterFacadeDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no chapter like that");

            foreach (var chapter in chapters.Items)
            {
                var story = await _context.Stories.FindAsync(chapter.IdStory);
                chapter.story = _mapper.Map<StoryDto>(story);
            }
           return chapters;

        }



    }
}
