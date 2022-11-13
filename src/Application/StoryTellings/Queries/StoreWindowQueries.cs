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
    public record StoreWindowQueries(int idTag,string user_id,int pgNumber):IRequest<PaginatedItems<StoryTellingDto>>;

    public class StoreWindowQueriesHandler : IRequestHandler<StoreWindowQueries, PaginatedItems<StoryTellingDto>>
    {
        private const int _pageSize = 50;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StoreWindowQueriesHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PaginatedItems<StoryTellingDto>> Handle(StoreWindowQueries request, CancellationToken cancellationToken)
        {
            var user = await _context.userEntities
               .Where(t => t.user_id == request.user_id)
               .SingleOrDefaultAsync() ?? throw new NotFoundException("No user Found");

            var tag = await getDefaultTag(request.idTag);

            var histoire = await _context.StoryTellings
                .Where(d=>d.idTag == tag)
                .ProjectTo<StoryTellingDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.price)
                .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken) ?? throw new NotFoundException("there is no image like that");

            return await ApplyForfaitReduction(user, histoire);
         
        }

        private async Task<PaginatedItems<StoryTellingDto>> ApplyForfaitReduction(UserEntity userEntity, PaginatedItems<StoryTellingDto> paginatedItems)
        {
            foreach (var item in paginatedItems.Items)
            {
                var price = item.price;
                item.price=await CalculPrice(userEntity,price);
            }
            return paginatedItems;
        }//si tu rajoute des tags et autre parametres dans forfait alors tu devras en prendre compte ici et dans transaction

        private async Task<double> CalculPrice(UserEntity userEntity, double price)
        {
                var forfaits = await GetForfaits(userEntity);
                var reduction =  GetReduction(forfaits);
                return price - (price * reduction) / 100;
        }
        private  double GetReduction(List<ForfaitClient> forfaitClients)
        {
            return  ChooseAppropriateForfeit(forfaitClients).Reduction;
        }
        private  ForfaitClient ChooseAppropriateForfeit(List<ForfaitClient> forfaits)
        {
            if (forfaits.Any(t => t.RoleId == 2))
                return forfaits
                    .SingleOrDefault(t=>t.RoleId==2);

            return forfaits
                    .SingleOrDefault();

        }

        private async Task<List<ForfaitClient>> GetForfaits(UserEntity userEntity)
        {
            var forfaitUser = await _context.Forfait_Users
                 .Where(t => t.IdUser == userEntity.IdUser)
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
    }


}
