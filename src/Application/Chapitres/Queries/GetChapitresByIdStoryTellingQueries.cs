using Application.Chapitres.Queries.Dto;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
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

namespace Application.Chapitres.Queries
{
    public record GetChapitresByIdStoryTellingQueries(int idStoryTell,int pgNumber): IRequest<PaginatedItems<ChapitresDto>>;

    public class GetChapitreByIdStoryTellingQueriesHandler : IRequestHandler<GetChapitresByIdStoryTellingQueries, PaginatedItems<ChapitresDto>>
    {
        private const int _pageSize = 4;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetChapitreByIdStoryTellingQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedItems<ChapitresDto>> Handle(GetChapitresByIdStoryTellingQueries request, CancellationToken cancellationToken)
        {
            var chapitres = await _context.Chapitres
                .Where(d => d.IdStoryTelling == request.idStoryTell)
                .ProjectTo<ChapitresDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Order)
                .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken);

            //await implementImage(chapitres);
            //await implementStory(chapitres);
            return chapitres;
        }


        //private async Task implementImage(PaginatedItems<ChapitresDto> chapitres) 
        //{
        // foreach(var item in chapitres.Items)
        //    {
        //        var result = await getImage(item.IdImage);
        //        item.Image =  _mapper.Map<ImageDto>(result);
        //    }   
        //}
        //private async Task implementStory(PaginatedItems<ChapitresDto> chapitres)
        //{
        //    foreach (var item in chapitres.Items)
        //    {
        //        var result = await getStory(item.IdStory);
        //        item.Story = _mapper.Map<StoryDto>(result);
        //    }
        //}


        //private async Task<Image> getImage(int id)
        //{
        //    return await  _context.Images.FindAsync(id) ?? throw new NotFoundException();
        //}
        //private async Task<Story> getStory(int id)
        //{
        //    return await _context.Stories.FindAsync(id) ?? throw new NotFoundException();
        //}
    }
}
