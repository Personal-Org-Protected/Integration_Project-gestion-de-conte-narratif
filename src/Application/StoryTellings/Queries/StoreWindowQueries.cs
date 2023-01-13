using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.StoryTellings.Queries.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public record StoreWindowQueries(int idTag,string user_id,int pgNumber) :IRequest<PaginatedItems<FacadeDto>>;

    public class StoreWindowQueriesHandler : IRequestHandler<StoreWindowQueries, PaginatedItems<FacadeDto>>
    {
        private const int _pageSize = 10;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly int _roleId=2;

        public StoreWindowQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedItems<FacadeDto>> Handle(StoreWindowQueries request, CancellationToken cancellationToken)
        {
            var tag = await getDefaultTag(request.idTag);
            var histoire = await _context.StoryTellings
                .Where(d=>d.idTag == tag && d.Finished) //changer ici
                .ProjectTo<FacadeDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.price)
                .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no book like that");
            return await ApplyForfaitReduction(request.user_id, histoire);
         
        }

        private async Task<PaginatedItems<FacadeDto>> ApplyForfaitReduction(string user_id, PaginatedItems<FacadeDto> paginatedItems)
        {
            foreach (var item in paginatedItems.Items)
            {
                await implementAuthor(item);
                var price = item.price;
                item.price=await CalculPrice(user_id,price);
            }
            return paginatedItems;
        }//si tu rajoute des tags et autre parametres dans forfait alors tu devras en prendre compte ici et dans transaction

        private async Task<double> CalculPrice(string user_id, double price)
        {
                var forfaits = await GetForfaits(user_id);
                var reduction =   GetReduction(forfaits);
                return price - (price * reduction) / 100;
        }
        private  double GetReduction(List<ForfaitClient> forfaitClients)
        {
            var forfait=   ChooseAppropriateForfeit(forfaitClients);
            return forfait.Reduction;
        }
        private  ForfaitClient ChooseAppropriateForfeit(List<ForfaitClient> forfaits)
        {
            var forfait=new ForfaitClient();
            forfait=forfaits.SingleOrDefault(t=>t.RoleId == _roleId && t.IdForfait>1);
            if(forfait==null)return forfaits.FirstOrDefault();
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
           return await  _context.Forfaits
                .Where(t => ids
                .Contains(t.IdForfait) && !t.IsForAuthor)
                .ToListAsync();
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

        private async Task implementAuthor(FacadeDto facade)
        {
            var user = await _context.Users.FindAsync(facade.user_id)
                ?? throw new NotFoundException("there is no user like that");
            facade.author = _mapper.Map<AuthorDto>(user);

        }
    }


}
