using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Forfaits.Command.CreateCommand
{
    public class CreateForfaitCommandValidator : AbstractValidator<CreateForfaitCommand>
    {
        public CreateForfaitCommandValidator()
        {
            RuleFor(v => v.ForfaitLibelle)
                .NotEmpty().WithMessage("A Forfait must have a name")
                .MaximumLength(20).WithMessage("name too long");
            RuleFor(v => v.IdRole)
                .NotEmpty().WithMessage("A Forfait must be linked to a user role");
            RuleFor(v => v.ForfaitValue)
                .NotEmpty().WithMessage("this Forfait must have a value")
                .GreaterThan(0).WithMessage("value must be positive");
            RuleFor(v => v.reduction)
               .GreaterThanOrEqualTo(0).WithMessage("value must be positive");
        }
    }
}
