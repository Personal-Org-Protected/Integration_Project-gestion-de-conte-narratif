using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stories.Command.CreateCommand
{
    public class CreateStoriesCommand:IRequest<Result>
    {
        public string NomStory { get; set; }
        public string TextStory { get; set; }

    }

    public class CreateStoriesResponseHandler : IRequestHandler<CreateStoriesCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        public CreateStoriesResponseHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(CreateStoriesCommand request, CancellationToken cancellationToken)
        {
            var entity = new Story()
            {
                NomStory = request.NomStory,
                TextStory = request.TextStory,
                DateCreation= DateTime.Now
            };
            await _context.Stories.AddAsync(entity);
            var resultTask=await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) return Result.Success("Story added with success",entity.IdStory);
            return Result.Failure("Action Failed : Story could not be added", new List<string>());
        }
    }
}
