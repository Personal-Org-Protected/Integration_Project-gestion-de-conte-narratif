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

namespace Application.Libraries.Command.CreateCommand
{
    public class CreateLibraryCommand :IRequest<Result>
    {
        public string NameLibrary { get; set; }
        public string user_id { get; set; }
    }

    public class CreateLibraryCommandHandler : IRequestHandler<CreateLibraryCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateLibraryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(CreateLibraryCommand request, CancellationToken cancellationToken)
        {
            var userEntity = await _context.userEntities
                .Where(t => t.user_id == request.user_id)
                .SingleOrDefaultAsync();
            var entity = new Library()
            {
                IdLibrary= "Library_"+userEntity.IdUser,
                NameLibrary = request.NameLibrary,
                IdUser = userEntity.IdUser,

            };
            _context.Libraries.Add(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0)
            {
                return Result.Success("Library added with success");
            }
            return Result.Failure("Action Failed : Library could not be added", new List<string>());
        }
    }
}
