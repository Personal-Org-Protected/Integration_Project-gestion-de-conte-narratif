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
            RuleFor(v=>v.user_id)
                .NotEmpty()
                .WithMessage("Need a user_id for the library");
        }


    }
}
