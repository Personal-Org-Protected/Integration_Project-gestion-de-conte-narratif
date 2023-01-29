using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.StoryTellings.Queries.Dto;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryTellings.Queries
{


    public record GetStoryTellFacadeByIdQueries(int idStoryTell,string user_id) : IRequest<FacadeDto>;

    public class GetStoryTellFacadeByIdQueriesHandler : IRequestHandler<GetStoryTellFacadeByIdQueries, FacadeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly int _roleId = 2;

        public GetStoryTellFacadeByIdQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FacadeDto> Handle(GetStoryTellFacadeByIdQueries request, CancellationToken cancellationToken)
        {
            var storyTells = await _context.StoryTellings.FindAsync(request.idStoryTell);
            var facade = _mapper.Map<FacadeDto>(storyTells);
            await implementAuthor(facade);
            var price = facade.price;
            facade.price = await CalculPrice(request.user_id, price);
            return  facade;
        }


        private async Task<double> CalculPrice(string user_id, double price)
        {
            var forfaits = await GetForfaits(user_id);
            var reduction = GetReduction(forfaits);
            return price - (price * reduction) / 100;
        }
        private async Task implementAuthor(FacadeDto facade)
        {
            var user = await _context.Users.FindAsync(facade.user_id)
                ?? throw new NotFoundException("there is no user like that");
            facade.author = _mapper.Map<AuthorDto>(user);

        }


        private double GetReduction(List<ForfaitClient> forfaitClients)
        {
            var forfait = ChooseAppropriateForfeit(forfaitClients);
            return forfait.Reduction;
        }
        private ForfaitClient ChooseAppropriateForfeit(List<ForfaitClient> forfaits)
        {
            var forfait = new ForfaitClient();
            forfait = forfaits.SingleOrDefault(t => t.RoleId == _roleId && t.IdForfait > 1);
            if (forfait == null) return forfaits.FirstOrDefault();
            return forfait;

        }

        private async Task<List<ForfaitClient>> GetForfaits(string user_id)
        {
            var forfaitUser = await _context.Forfait_Users
                 .Where(t => t.user_id == user_id)
                 .ToListAsync();
            var ids = new List<int>();
            foreach (var item in forfaitUser)
            {
                ids.Add(item.IdForfait);
            }
            return await _context.Forfaits
                 .Where(t => ids
                 .Contains(t.IdForfait) && !t.IsForAuthor)
                 .ToListAsync();
        }
    }
}
