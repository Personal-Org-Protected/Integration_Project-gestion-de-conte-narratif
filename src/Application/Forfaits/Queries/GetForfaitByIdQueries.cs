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

namespace Application.Forfaits.Queries
{
    public record GetForfaitByIdQueries(int id):IRequest<ForfaitDto>;

    public class GetForfaitByIdQueriesHandler : IRequestHandler<GetForfaitByIdQueries, ForfaitDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetForfaitByIdQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ForfaitDto> Handle(GetForfaitByIdQueries request, CancellationToken cancellationToken)
        {
            var entity = await _context.Forfaits
               .Where(t => t.IdForfait == request.id)
               .FirstOrDefaultAsync() ?? throw new NotFoundException("Chapitre", request);

            return _mapper.Map<ForfaitDto>(entity);
        }
    }

}
