using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryTellings.Command.CreateCommand
{
    public class CreateStoryTellingCommand : IRequest<Result>
    {
        public string? url { get; set; }//new
        public string NameStory { get; set; }
        public double price { get; set; }
        public string synopsis { get; set; }
        public int idTag { get; set; }
    }

    public class CreateStoryTellingCommandHandler : IRequestHandler<CreateStoryTellingCommand, Result>
    {

        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public CreateStoryTellingCommandHandler(IApplicationDbContext context,IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<Result> Handle(CreateStoryTellingCommand request, CancellationToken cancellationToken)
        {
            var user_id = _user.getUserId();
            var zoneCommentaire = await CreateZoneComm(cancellationToken);
            var tag = await getDefaultTag(request.idTag);
            var entity = new StoryTelling()
            {
                NameStory = request.NameStory,
                url = request.url,
                user_id=user_id,
                DateCreation=DateTime.Now,
                price = request.price,
                Sypnopsis=request.synopsis,
                IdZone= zoneCommentaire,
                idTag=tag,
                rating=0
            };
            await _context.StoryTellings.AddAsync(entity);
            await changeTagCount(tag);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 1) return Result.Success("StoryTell added with success");
            return Result.Failure("Action Failed : StoryTell could not be added", new List<string>());
        }

        private async Task<int> CreateZoneComm(CancellationToken cancellationToken)
        {
            var ZoneComm = _context.ZoneComments.Add(new ZoneCommentary());
            var result = await _context.SaveChangesAsync(cancellationToken);
            if (result == 0) throw new Exception();

            return ZoneComm.Entity.IdZone;
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
        private async Task changeTagCount(int idTag)
        {
            var tag = await _context.Tag
                .FindAsync(idTag);
            tag.numberRef++;
            _context.Tag.Update(tag);
        }
    }
}
