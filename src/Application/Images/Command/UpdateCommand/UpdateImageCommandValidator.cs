using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Images.Command.UpdateCommand
{
    public class UpdateImageCommandValidator : AbstractValidator<UpdateImageCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateImageCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v=>v.IdImage)
                .NotEmpty().WithMessage("Id image can not be empty")
                .GreaterThan(0).WithMessage("id image can not be negative");
            RuleFor(v => v.NomImage)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(40).WithMessage("Name must not exceed 40 characters.");
            RuleFor(v => v.descriptionImage)
                .MaximumLength(400).WithMessage("Desciption must not exceed 400 characters.");
        }

    }
}
