using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chapitres.Command.CreateCommand
{
    public class CreateChapitresCommandValidator : AbstractValidator<CreateChapitresCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateChapitresCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v=>v.IdImage)
                .NotEmpty().WithMessage("an image must be chosen");
            RuleFor(v => v.IdStory)
                .NotEmpty().WithMessage("A story must be created");
            RuleFor(v => v.IdStoryTelling)
                .NotEmpty().WithMessage("this chapter must be linked to a storyTell");
            RuleFor(v => v.Order)
                .GreaterThanOrEqualTo(0)
                .WithMessage("what pg number is this chapter");

        }

    }
}
