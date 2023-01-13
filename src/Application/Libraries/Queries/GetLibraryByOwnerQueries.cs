using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Libraries.Queries
{
    public record GetLibraryByOwnerQueries(string owner) :IRequest<LibrariesDto>;

    public class GetLibraryByOwnerQueryHandler : IRequestHandler<GetLibraryByOwnerQueries, LibrariesDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLibraryByOwnerQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<LibrariesDto> Handle(GetLibraryByOwnerQueries request, CancellationToken cancellationToken)
        {
            var library = await _context.Libraries
                .Where(t => t.user_id == request.owner) //creer un moel qui recupere la librairie et les hitsoires a l' interieur
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Library", request);

            return _mapper.Map<LibrariesDto>(library);

            //libraryDto.StoryTellBoxDtos=await _context.storyTellBoxes
            //    .Where(t=>t.IdLibrary==library.IdLibrary)
            //    .ProjectTo<StoryTellBoxDto>(_mapper.ConfigurationProvider)
            //    .ToListAsync();

            //return libraryDto;
        }
    }
}
