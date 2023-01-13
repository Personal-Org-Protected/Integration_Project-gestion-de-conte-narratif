using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryTellings.Command.CreateCommand
{
    internal class CreateStoryTellingCommandValidator : AbstractValidator<CreateStoryTellingCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateStoryTellingCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            
            RuleFor(v => v.NameStory)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(20).WithMessage("Name must not exceed 20 characters.");

            RuleFor(v => v.price)
               .NotEmpty().WithMessage("price is required.")
               .GreaterThanOrEqualTo(0)
               .WithMessage("can not be below 0");

            RuleFor(v => v.url)
             .MaximumLength(200)
             .WithMessage("should allow between 0 and 200 characte");

            RuleFor(v => v.synopsis)
             .NotEmpty().WithMessage("synopsis is required.")
             .MinimumLength(20)
             .MaximumLength(300)
             .WithMessage("should allow between 20 and 300 character");

            RuleFor(v => v.synopsis)
               .NotEmpty().WithMessage("synopsis is required.")
               .MaximumLength(400).WithMessage("synopsis must not exceed 400 characters.");
            RuleFor(v => v.idTag)
             .GreaterThanOrEqualTo(0).WithMessage("need a existing tag");


        }
    }
}
