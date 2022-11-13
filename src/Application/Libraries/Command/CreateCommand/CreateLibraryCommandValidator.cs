using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Libraries.Command.CreateCommand
{
    public class CreateLibraryCommandValidator : AbstractValidator<CreateLibraryCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateLibraryCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(v=>v.NameLibrary)
                .NotEmpty()
                .WithMessage("Need a name for the library");
            RuleFor(v=>v.user_id)
                .NotEmpty()
                .WithMessage("This library must belong to someone")
                .MustAsync((x, cancellationToken) => CheckIfUserExist(x, cancellationToken)).WithMessage("User does not  exist ");
        }


      public async Task<bool> CheckIfUserExist(string user_id, CancellationToken cancellationToken)
        {
            var user = await _context.userEntities
                  .Where(t => t.user_id == user_id)
                  .SingleOrDefaultAsync();
            return await _context
                .userEntities
                .AnyAsync(t => t.IdUser ==user.IdUser , cancellationToken);
        }
    }
}
