using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ideas.Command.CreateCommand
{
    public class CreateideaCommandValidator : AbstractValidator<CreateIdeaCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateideaCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.idea)
                .NotEmpty().WithMessage("Idea is required.")
                .MaximumLength(200).WithMessage("Ideas must not exceed 200 characters.");
            RuleFor(v => v.StoryId)
                .NotEmpty().WithMessage("what story is linked to");

        }
    }
}
