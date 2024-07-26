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
    public record GetStoryBoxByUserQueries(string IdLibrary,int pgNumber) : IRequest<PaginatedItems<StoryBoxesDto>>;


    public class GetStoryBoxByUserQueriesHandler : IRequestHandler<GetStoryBoxByUserQueries, PaginatedItems<StoryBoxesDto>>
    {
        private const int _pageSize = 6;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStoryBoxByUserQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedItems<StoryBoxesDto>> Handle(GetStoryBoxByUserQueries request, CancellationToken cancellationToken)
        {
            var Boxes= await _context.storyTellBoxes.Where(t => t.IdLibrary == request.IdLibrary)
                .ProjectTo<StoryBoxesDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.IdBox)
                .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no book like that");
             await implementFacade(Boxes);
            return Boxes;
        }

        private async Task implementFacade(PaginatedItems<StoryBoxesDto> paginatedItems)
        {
            foreach(var item in paginatedItems.Items)
            {
                var storyTells = await _context.StoryTellings.FindAsync(item.IdStoryTell);
                var facade= _mapper.Map<FacadeDto>(storyTells);
                item.facade = facade;
                await implementAuthor(facade);
            }
        }

       private async Task implementAuthor(FacadeDto facade)
        {
            var user = await _context.Users.FindAsync(facade.user_id) 
                ?? throw new NotFoundException("there is no user like that");
            facade.author = _mapper.Map<AuthorDto>(user);

        }
    }
}
