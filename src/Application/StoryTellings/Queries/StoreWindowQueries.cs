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
    public record StoreWindowQueries(int idTag,int pgNumber, string? search=" ", bool? rate=false,bool? price=false) :IRequest<PaginatedItems<FacadeDto>>;

    public class StoreWindowQueriesHandler : IRequestHandler<StoreWindowQueries, PaginatedItems<FacadeDto>>
    {
        private const int _pageSize = 8;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly int _roleId=2;
        private readonly IUser _user;

        public StoreWindowQueriesHandler(IApplicationDbContext context, IMapper mapper,IUser user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }
        public async Task<PaginatedItems<FacadeDto>> Handle(StoreWindowQueries request, CancellationToken cancellationToken)
        {
            var user_id =_user.getUserId();
            var tag = await getDefaultTag(request.idTag);
            var histoire = await GetBooks(request,tag,cancellationToken); 
            /* await _context.StoryTellings
                .Where(d => d.idTag == tag && d.Finished) //changer ici
                .ProjectTo<FacadeDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.price)
                .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken);*/
            return await ApplyForfaitReduction(user_id, histoire);
         
        }

        private async Task<PaginatedItems<FacadeDto>> ApplyForfaitReduction(string user_id, PaginatedItems<FacadeDto> paginatedItems)
        {
            foreach (var item in paginatedItems.Items)
            {
                await implementAuthor(item);
            }
            return paginatedItems;
        }//si tu rajoute des tags et autre parametres dans forfait alors tu devras en prendre compte ici et dans transaction

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
        private async Task<PaginatedItems<FacadeDto>> GetBooks(StoreWindowQueries request ,int tag,CancellationToken cancellationToken)
        {
            if(request.search!=null && request.search.Count() > 0 && tag > await getDefaultTag(0)) {
               return await getResearchTag(request, tag, cancellationToken);
            }
            else if(request.search != null && request.search.Count() > 0)
            {
                return await getResearch(request, cancellationToken);
            }
            var histoires = await _context.StoryTellings
                .Where(d => d.Finished) //changer ici
                .ProjectTo<FacadeDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.DateCreation)
                .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken);
            return histoires;
        }

        private async Task<PaginatedItems<FacadeDto>> getResearchTag(StoreWindowQueries request, int tag, CancellationToken cancellationToken)
        {
            if (request.rate.Value)
            {
                var rated = await _context.StoryTellings
                   .Where(d => d.idTag == tag && d.Finished && d.NameStory.Contains(request.search)) //changer ici
                   .ProjectTo<FacadeDto>(_mapper.ConfigurationProvider)
                   .OrderBy(t => t.rating)
                   .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken);
                return rated;
            }
            else if (request.price.Value)
            {
                var prices = await _context.StoryTellings
                   .Where(d => d.idTag == tag && d.Finished && d.NameStory.Contains(request.search)) //changer ici
                   .ProjectTo<FacadeDto>(_mapper.ConfigurationProvider)
                   .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken);
                return prices;
            }

            var researches = await _context.StoryTellings
                   .Where(d => d.idTag == tag && d.Finished && d.NameStory.Contains(request.search)) //changer ici
                   .ProjectTo<FacadeDto>(_mapper.ConfigurationProvider)
                   .OrderBy(t => t.DateCreation)
                   .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken);
            return researches;
        }

        private async Task<PaginatedItems<FacadeDto>> getResearch(StoreWindowQueries request, CancellationToken cancellationToken)
        {
            if (request.rate.Value)
            {
                var rated = await _context.StoryTellings
                   .Where(d => d.Finished && d.NameStory.Contains(request.search)) //changer ici
                   .ProjectTo<FacadeDto>(_mapper.ConfigurationProvider)
                   .OrderBy(t => t.rating)
                   .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken);
                return rated;
            }
            else if (request.price.Value)
            {
                var prices = await _context.StoryTellings
                   .Where(d =>  d.Finished && d.NameStory.Contains(request.search)) //changer ici
                   .ProjectTo<FacadeDto>(_mapper.ConfigurationProvider)
                   .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken);
                return prices;
            }

            var researches = await _context.StoryTellings
                   .Where(d => d.Finished && d.NameStory.Contains(request.search)) //changer ici
                   .ProjectTo<FacadeDto>(_mapper.ConfigurationProvider)
                   .OrderBy(t => t.DateCreation)
                   .PaginatedListAsync(request.pgNumber, _pageSize, cancellationToken);
            return researches;
        }

        private double verifyFree(double alteredValue)
        {
            if (alteredValue < 0)
                return 0;
            else return alteredValue;
        }

    }


}
