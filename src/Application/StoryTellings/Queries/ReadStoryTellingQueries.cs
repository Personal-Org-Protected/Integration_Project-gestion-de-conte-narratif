using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.StoryTellings.Queries.Dto;
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

namespace Application.StoryTellings.Queries
{
    public record ReadStoryTellingQueries(int idStoryTell,string idLibrary) : IRequest<StoryTellBoxDto>;


    public class ReadStoryTellingQueriesdHandler : IRequestHandler<ReadStoryTellingQueries, StoryTellBoxDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReadStoryTellingQueriesdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<StoryTellBoxDto> Handle(ReadStoryTellingQueries request, CancellationToken cancellationToken)
        {

            var BoxStories = await _context.storyTellBoxes
                .Where(d =>d.IdLibrary==request.idLibrary && d.IdStoryTell==request.idStoryTell) // a voir => changer si necessaire 
                .FirstOrDefaultAsync() ?? throw new NotFoundException("StorieBox", request);

            var BoxDto= _mapper.Map<StoryTellBoxDto>(BoxStories);
             return await implementChapters(request.idStoryTell,BoxDto);

        }

        private async Task<StoryTellBoxDto> implementChapters(int stroryTell,StoryTellBoxDto box)
        {
            var chapters=await _context.Chapitres
                .Where(t=>t.IdStoryTelling==stroryTell)
                .ToListAsync();
            box.chapitres = new List<ChapitreDto>();
            foreach(var chapter in chapters)
            {
                var chap = await implementChapter(chapter);
                box.chapitres.Add(chap);
            }
            return box;
        }

        private async Task<ChapitreDto> implementChapter(Chapitre chapitre)
        {
            var story = await getnarration(chapitre.IdStory);
            var image= await getImage(chapitre.IdImage);
            return new ChapitreDto()
            {
                IdChapitre=chapitre.IdChapitre,
                Order=chapitre.Order,
                ChapitreName=story.NomStory,
                narration=story.TextStory,
                nomImage=image.NomImage,
                descriptionImage=image.descriptionImage,
                path=image.PathImage,
                url=image.Uri

            };
        }
        private async Task<Image> getImage(int id)
        {
            return await _context.Images.FindAsync(id)?? throw new NotFoundException("Image not found for the storyBox");
        }
        private async Task<Story> getnarration(int id)
        {
            return await _context.Stories.FindAsync(id) ?? throw new NotFoundException("story not found for the storyBox");
        }
    }
}
