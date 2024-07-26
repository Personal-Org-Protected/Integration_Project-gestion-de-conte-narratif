using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stories.Command.CreateCommand
{
    public class CreateStoriesCommandValidator : AbstractValidator<CreateStoriesCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateStoriesCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.NomStory)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(40).WithMessage("Name must not exceed 100 characters.");
            RuleFor(v => v.TextStory)
                .MaximumLength(1000).WithMessage("Desciption must not exceed 1000 characters.");
        }
    }
}
