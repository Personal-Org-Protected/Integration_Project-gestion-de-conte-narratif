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
    public record GetImageByOwnerQueries(string user_id,int pgNumber, int idTag) :IRequest<PaginatedItems<ImageDto>>;

    public class GetImageByOwnerQueriesHandler : IRequestHandler<GetImageByOwnerQueries, PaginatedItems<ImageDto>>
    {
        private const int _pageSize = 4;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetImageByOwnerQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedItems<ImageDto>> Handle(GetImageByOwnerQueries request, CancellationToken cancellationToken)
        {
            if (request.idTag != 0)
                return await getSpec(request.user_id, request.idTag, request.pgNumber, cancellationToken);


            return await getAll(request.user_id, request.pgNumber, cancellationToken);
              
        }

        public async Task<PaginatedItems<ImageDto>> getAll(string user_id,int pgNumber,CancellationToken cancellationToken)
        {
            return await _context.Images
            .Where(d => d.user_id == user_id)
            .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
            .OrderBy(t => t.IdImage)
            .PaginatedListAsync(pgNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no image like that");
        }


        public async Task<PaginatedItems<ImageDto>> getSpec(string user_id, int idTag,int pgNumber, CancellationToken cancellationToken)
        {
                return await _context.Images
                    .Where(d => d.user_id == user_id && d.IdTag == idTag)
                    .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
                    .OrderBy(t => t.IdImage)
                    .PaginatedListAsync(pgNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no image like that");
        }



        //private async Task<int> getDefaultTag(int idTag)
        //{
        //    if (idTag == 0)
        //    {
        //        var result = await _context.Tag
        //                           .Where(t => t.NameTag == "Default")
        //                           .SingleOrDefaultAsync() ?? throw new NotFoundException("no tag by default found"); idTag = result.IdTag;
        //    }
        //    return idTag;
        //}
    }
}
