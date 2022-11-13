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
    public record GetImagesQueries(int pageNumber) : IRequest<PaginatedItems<ImageDto>>;
    public class GetImagesQueriesHandler : IRequestHandler<GetImagesQueries, PaginatedItems<ImageDto>>
    {
        private int _pageSize { get; set; } = 50;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetImagesQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedItems<ImageDto>> Handle(GetImagesQueries request, CancellationToken cancellationToken)
        {
            return await _context.Images
                .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.NomImage)
                .PaginatedListAsync(request.pageNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no StoryTelling available");

               
        }
    }

}
