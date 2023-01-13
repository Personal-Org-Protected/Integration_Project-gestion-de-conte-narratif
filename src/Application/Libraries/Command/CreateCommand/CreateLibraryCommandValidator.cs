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
            RuleFor(v => v.user_id)
                .NotEmpty()
                .WithMessage("This library must belong to someone");
        }


    }
}
