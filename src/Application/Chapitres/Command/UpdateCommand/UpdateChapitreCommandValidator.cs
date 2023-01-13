using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Chapitres.Command.UpdateCommand
{
    public class UpdateChapitreCommandValidator : AbstractValidator<UpdateChapitreCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateChapitreCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.idImage)
                .NotEmpty().WithMessage("an image must be given");

        }
    }
}
