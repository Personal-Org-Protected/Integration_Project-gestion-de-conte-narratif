using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.StoryBox.Query.Dto;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryBox.Query
{
    
    public record GetStoryBoxeReadQueries(int idStoryTell,int order) : IRequest<ChapterDto>;

    public class GetStoryBoxeReadQueriesHandler : IRequestHandler<GetStoryBoxeReadQueries, ChapterDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStoryBoxeReadQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ChapterDto> Handle(GetStoryBoxeReadQueries request, CancellationToken cancellationToken)
        {
            var chapitre = await _context.Chapitres
               .Where(t => t.IdStoryTelling == request.idStoryTell && t.Order==request.order)
               .SingleOrDefaultAsync()?? throw new NotFoundException("no chapter like that");
            var chapter=_mapper.Map<ChapterDto>(chapitre);
            await implementStory(chapter);
            await implementImage(chapter);
            return chapter;
        }


        private async Task implementStory(ChapterDto chapter)
        {
            chapter.Story = await implementNarration(chapter.IdStory);
        }
        private async Task implementImage(ChapterDto chapter)
        {
            chapter.Image = await implementImage(chapter.IdImage);
        }

        private async Task<ImageDto> implementImage(int id)
        {
            var image= await _context.Images.FindAsync(id) ?? throw new NotFoundException("Image not found for the storyBox");
            return _mapper.Map<ImageDto>(image);
        }
        private async Task<StoryDto> implementNarration(int id)
        {
            var story= await _context.Stories.FindAsync(id) ?? throw new NotFoundException("story not found for the storyBox");
            return _mapper.Map<StoryDto>(story);
        }

    }
}
