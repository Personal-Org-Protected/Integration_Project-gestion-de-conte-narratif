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

namespace Application.Chapitres.Queries
{
    public record GetChapitresByIdStoryTellingQueries(int idStoryTell,int pgNumber): IRequest<PaginatedItems<ChapitresDto>>;

    public class GetChapitreByIdStoryTellingQueriesHandler : IRequestHandler<GetChapitresByIdStoryTellingQueries, PaginatedItems<ChapitresDto>>
    {
        private const int _pageSize = 50;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetChapitreByIdStoryTellingQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedItems<ChapitresDto>> Handle(GetChapitresByIdStoryTellingQueries request, CancellationToken cancellationToken)
        {
            return await _context.Chapitres
                .Where(d=>d.IdStoryTelling==request.idStoryTell)
                .ProjectTo<ChapitresDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.IdChapitre)
                .PaginatedListAsync(request.pgNumber, _pageSize,cancellationToken) ?? throw new NotFoundException("there is no Chapter available");
        }
    }
}
