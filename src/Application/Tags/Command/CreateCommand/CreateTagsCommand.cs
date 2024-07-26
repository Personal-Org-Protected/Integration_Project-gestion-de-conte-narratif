using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tags.Command.CreateCommand
{
    public class CreateTagsCommand:IRequest<Result>
    {
         public string NameTag { get; set; }
    }

    public class CreateTagsCommandHandler : IRequestHandler<CreateTagsCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateTagsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(CreateTagsCommand request, CancellationToken cancellationToken)
        {
            var entity = new Tag()
            {
                NameTag = request.NameTag  
            };
            await _context.Tag.AddAsync(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) return Result.Success("Tag added with success");
            return Result.Failure("Action Failed : Tag could not be added", new List<string>());
        }
    }
}
