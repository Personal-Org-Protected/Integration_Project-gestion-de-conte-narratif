using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.StoryBox.Query.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryBox.Query
{
    public record GetStoryBoxByIdQueries(int idBox) :IRequest<StoryBoxesDto>;

    public class GetStoryBoxByIdQueriesHandler : IRequestHandler<GetStoryBoxByIdQueries, StoryBoxesDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStoryBoxByIdQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StoryBoxesDto> Handle(GetStoryBoxByIdQueries request, CancellationToken cancellationToken)
        {
            var boxe=await _context.storyTellBoxes.FindAsync(request.idBox)?? throw new NotFoundException("No story Box like that");
            var boxDto= _mapper.Map<StoryBoxesDto>(boxe);
            await implementFacade(boxDto);
            return boxDto;
        }
        private async Task implementFacade(StoryBoxesDto storyBox)
        {
                var storyTells = await _context.StoryTellings.FindAsync(storyBox.IdStoryTell);
                var facade = _mapper.Map<FacadeDto>(storyTells);
                await implementAuthor(facade);
            storyBox.facade = facade;
        }

        private async Task implementAuthor(FacadeDto facade)
        {
            var user = await _context.Users.FindAsync(facade.user_id)
                ?? throw new NotFoundException("there is no user like that");
            facade.author = _mapper.Map<AuthorDto>(user);

        }

    }
}
