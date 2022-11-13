using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Images.Command.CreateCommand
{
    public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateImageCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.NomImage)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(40).WithMessage("Name must not exceed 40 characters.")
                .MustAsync((x, cancellationToken) => NameAlreadyTaken(x, cancellationToken)).WithMessage("Name exist already");
            RuleFor(v => v.descriptionImage)
                .MaximumLength(500).WithMessage("Desciption must not exceed 500 characters.");
            RuleFor(v=>v.Uri)
                .NotEmpty().WithMessage("An uri must be passed")
                .MustAsync((x, cancellationToken) => ImageAlreadyExist(x, cancellationToken)).WithMessage("Uri exist already");

        }

        private async Task<bool> ImageAlreadyExist(string uri, CancellationToken cancellationToken)
        {
            return !await _context.Images
                .AnyAsync(t=>t.Uri==uri,cancellationToken);
        }
        private async Task<bool> NameAlreadyTaken(string nameImage, CancellationToken cancellationToken)
        {
            return !await _context.Images
                .AnyAsync(t => t.NomImage == nameImage, cancellationToken);
        }
    }
}
