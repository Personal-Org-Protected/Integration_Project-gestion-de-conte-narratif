using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Images.Queries
{
    public record GetImageByOwnerQueries(string user_id,int pgNumber, int idTag) :IRequest<PaginatedItems<ImageDto>>;

    public class GetImageByOwnerQueriesHandler : IRequestHandler<GetImageByOwnerQueries, PaginatedItems<ImageDto>>
    {
        private const int _pageSize = 50;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetImageByOwnerQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedItems<ImageDto>> Handle(GetImageByOwnerQueries request, CancellationToken cancellationToken)
        {
            var user = await _context.userEntities
                .Where(t => t.user_id == request.user_id)
                .SingleOrDefaultAsync()?? throw new NotFoundException("No user Found");
            var tag = await getDefaultTag(request.idTag);
            return await _context.Images
                .Where(d => d.owner == user.IdUser && d.IdTag==tag)
                .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.IdImage)
                .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no image like that");
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
