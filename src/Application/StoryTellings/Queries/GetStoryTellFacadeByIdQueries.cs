﻿using Application.Common.Exceptions;
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


    public record GetStoryTellFacadeByIdQueries(int idStoryTell) : IRequest<FacadeDto>;

    public class GetStoryTellFacadeByIdQueriesHandler : IRequestHandler<GetStoryTellFacadeByIdQueries, FacadeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly int _roleId = 2;
        private readonly IUser _user;

        public GetStoryTellFacadeByIdQueriesHandler(IApplicationDbContext context, IMapper mapper,IUser user)
        {
            _context = context;
            _mapper = mapper;
            _user = user;
        }

        public async Task<FacadeDto> Handle(GetStoryTellFacadeByIdQueries request, CancellationToken cancellationToken)
        {
            var user_id = _user.getUserId();
            var storyTells = await _context.StoryTellings.FindAsync(request.idStoryTell);
            var facade = _mapper.Map<FacadeDto>(storyTells);
            await implementAuthor(facade);
            return  facade;
        }


        private async Task implementAuthor(FacadeDto facade)
        {
            var user = await _context.Users.FindAsync(facade.user_id)
                ?? throw new NotFoundException("there is no user like that");
            facade.author = _mapper.Map<AuthorDto>(user);

        }



    }
}
