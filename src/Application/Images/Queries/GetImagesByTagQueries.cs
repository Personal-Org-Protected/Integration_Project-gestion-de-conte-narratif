using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Images.Queries.Dto;
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
    public record GetImagesByTagQueries (int Tag,int pageNumber):IRequest<PaginatedItems<ImageDto>>;

    public class GetImagesByTagQueriesHandler : IRequestHandler<GetImagesByTagQueries, PaginatedItems<ImageDto>>
    {
        private const int _pageSize = 50;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetImagesByTagQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedItems<ImageDto>> Handle(GetImagesByTagQueries request, CancellationToken cancellationToken)
        {
            var idTag = _context.Tag.Where(t => t.IdTag == request.Tag).FirstOrDefault() ?? 
                throw new NotFoundException("Tag", request.Tag);

            return await _context.Images
                .Where(t => t.IdTag == idTag.IdTag)
                .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.NomImage)
                .PaginatedListAsync(request.pageNumber, _pageSize, cancellationToken);
       
        }
    }
}
