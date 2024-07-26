using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Stories.Command.UpdateCommand
{
    public class UpdateStoriesCommandValidator : AbstractValidator<UpdateStoriesCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateStoriesCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(v=>v.IdStory)
                .NotEmpty().WithMessage("Id Story can not be empty")
                .GreaterThan(0).WithMessage("id Story can not be negative");
            RuleFor(v => v.NomStory)
                   .NotEmpty().WithMessage("Name is required.")
                   .MaximumLength(40).WithMessage("Name must not exceed 40 characters.");
            RuleFor(v => v.TextStory)
                .MaximumLength(1000).WithMessage("Desciption must not exceed 1000 characters.");
        }
    }
}
