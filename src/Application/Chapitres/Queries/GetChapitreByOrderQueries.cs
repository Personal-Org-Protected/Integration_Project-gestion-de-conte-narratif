using Application.Chapitres.Queries.Dto;
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
    public record GetChapitreByOrderQueries(int idStoryTell,int order) : IRequest<ChapitreOrderDto>;

    public class GetChapitreByOrderQueriesHandler : IRequestHandler<GetChapitreByOrderQueries, ChapitreOrderDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetChapitreByOrderQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ChapitreOrderDto> Handle(GetChapitreByOrderQueries request, CancellationToken cancellationToken)
        {
            var entity = await _context.Chapitres
                .Where(t => t.IdStoryTelling==request.idStoryTell && t.Order == request.order)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Chapitre", request);
            var chapter= _mapper.Map<ChapitreOrderDto>(entity);
            await checkStateOrder(chapter,request.idStoryTell);
            return chapter;
        }

        private async Task checkStateOrder(ChapitreOrderDto chapitreOrder,int idStoryTell)
        {
            var chapters = await _context.Chapitres
                .Where(t => t.IdStoryTelling == idStoryTell)
                .ToListAsync();
            chapitreOrder.hasNext = chapters.Any(t => t.Order > chapitreOrder.Order);
            chapitreOrder.hasPrevious=chapters.Any(t=>t.Order < chapitreOrder.Order);
        }
    }
}
