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

namespace Application.Chapitres.Queries
{
    public record GetChapitresByIdQueries(int id): IRequest<ChapitresDto>;

    public class GetChapitresByIdsHandler : IRequestHandler<GetChapitresByIdQueries, ChapitresDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetChapitresByIdsHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ChapitresDto> Handle(GetChapitresByIdQueries request, CancellationToken cancellationToken)
        {
            var entity = await _context.Chapitres
                .Where(t => t.IdChapitre == request.id)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Chapitre",request);
            return _mapper.Map<ChapitresDto>(entity);
        }
    }
}
