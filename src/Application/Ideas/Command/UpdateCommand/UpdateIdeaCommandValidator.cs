using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ideas.Command.UpdateCommand
{
    public class UpdateIdeaCommandValidator : AbstractValidator<UpdateIdeaCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateIdeaCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.idIdea)
                .NotEmpty().WithMessage("Id image can not be empty")
                .GreaterThan(0).WithMessage("id image can not be negative");
            RuleFor(v => v.idea)
                .NotEmpty().WithMessage("idea is required.")
                .MaximumLength(200).WithMessage("idea must not exceed 200 characters.");
        }
    }
}
