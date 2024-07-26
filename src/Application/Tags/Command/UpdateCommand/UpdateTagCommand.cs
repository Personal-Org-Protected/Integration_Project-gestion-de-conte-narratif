using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tags.Command.UpdateCommand
{
    public class UpdateTagCommand : IRequest<Result>
    {
        public int IdTag { get; set; }
        public string NameTag { get; set; }
        
    }

    public class UpdateTagsCommandHandler : IRequestHandler<UpdateTagCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTagsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tag
            .FindAsync(new object[] { request.IdTag }, cancellationToken) ?? throw new NotFoundException(nameof(Tag), request.IdTag);

            entity.NameTag = request.NameTag;
            _context.Tag.Update(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0)
            {
                return Result.Success("Tag modified with success");
            }
            return Result.Failure("Action Failed : Tag could not be modified", new List<string>());
        }
    }
}
