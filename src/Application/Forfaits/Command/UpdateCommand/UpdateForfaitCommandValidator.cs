using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Forfaits.Command.UpdateCommand
{
    public class UpdateForfaitCommandValidator : AbstractValidator<UpdateForfaitCommand>
    {
        public UpdateForfaitCommandValidator()
        {

            RuleFor(v => v.ForfaitLibelle)
                .NotEmpty().WithMessage("A Forfait must have a name")
                .MaximumLength(20).WithMessage("name too long");
            RuleFor(v => v.IdForfait)
                .NotEmpty().WithMessage("A Forfait must have an id");
            RuleFor(v => v.ForfaitValue)
                .NotEmpty().WithMessage("this Forfait must have a value")
                .GreaterThan(0).WithMessage("value must be positive");
        }
    }
}
