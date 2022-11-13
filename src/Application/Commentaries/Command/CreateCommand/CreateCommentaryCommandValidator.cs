using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commentaries.Command.CreateCommand
{
    public class CreateCommentaryCommandValidator : AbstractValidator<CreateCommentaryCommand>
    {
        public CreateCommentaryCommandValidator()
        {
            RuleFor(v => v.Commentaire)
                .NotEmpty().WithMessage("Commentary must be written")
                .MaximumLength(200).WithMessage("must not exceed 200 characters");
            RuleFor(v => v.Owner)
                .NotEmpty().WithMessage("commentary must have an owner");
            RuleFor(v => v.IdZone)
               .NotEmpty().WithMessage("this commentary must be linked to a storyTell");
        }

    }
}
