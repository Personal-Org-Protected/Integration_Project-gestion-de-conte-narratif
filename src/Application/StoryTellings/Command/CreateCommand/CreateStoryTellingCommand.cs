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
        public string IdUser { get; set; }
        public double price { get; set; }
        public string synopsis { get; set; }
        public int idTag { get; set; }
    }

    public class CreateStoryTellingCommandHandler : IRequestHandler<CreateStoryTellingCommand, Result>
    {

        private readonly IApplicationDbContext _context;

        public CreateStoryTellingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(CreateStoryTellingCommand request, CancellationToken cancellationToken)
        {


            var entity = new StoryTelling()
            {
                NameStory = request.NameStory,
                url = request.url,
                IdUser=request.IdUser,
                DateCreation=DateTime.Now,
                price = request.price,
                Sypnopsis=request.synopsis,
                IdZone=await CreateZoneComm(cancellationToken),
                idTag=await getDefaultTag(request.idTag),
            };
            _context.StoryTellings.Add(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) return Result.Success("StoryTell added with success");
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
    }
}
