using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StoryTellings.Command.UpdateCommand
{
    public class UpdateStoryTellingCommandValidator :  AbstractValidator<UpdateStoryTellingCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateStoryTellingCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(v => v.Id).NotEmpty().WithMessage("must give an id");
            RuleFor(v => v.NameStory)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(40).WithMessage("Name must not exceed 100 characters.");
            RuleFor(v => v.price)
                .NotEmpty().WithMessage("a price must given")
                .GreaterThan(0).WithMessage("price can not be negative");
            RuleFor(v => v.url)
                .MaximumLength(200)
                .WithMessage("too long url");
            RuleFor(v => v.synopsis)
                .NotNull()
                .WithMessage("must have a synopsis")
                .MaximumLength(400)
                .WithMessage("must not exceed 400 character");
        }
    }
}
