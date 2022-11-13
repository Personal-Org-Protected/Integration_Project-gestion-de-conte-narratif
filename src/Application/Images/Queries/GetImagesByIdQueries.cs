using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Images.Queries
{
    public record GetImagesByIdQueries(int id):IRequest<ImageDto>;
    public class GetImagesByIdQueriesHandler : IRequestHandler<GetImagesByIdQueries, ImageDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetImagesByIdQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ImageDto> Handle(GetImagesByIdQueries request, CancellationToken cancellationToken)
        {
            var Image = await _context.Images
                .Where(t => t.IdImage == request.id)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Image",request);
            return _mapper.Map<ImageDto>(Image);
        }
    }
}
