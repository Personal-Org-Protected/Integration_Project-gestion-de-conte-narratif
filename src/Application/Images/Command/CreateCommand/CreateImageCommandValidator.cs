using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using nClam;
using NsfwSpyNS;

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
                .Matches("")
                .MustAsync((x, cancellationToken) => UriIsValid(x, cancellationToken)).WithMessage("Uri exist already")
                .MustAsync((x, cancellationToken) => IsImageClean(x, cancellationToken)).WithMessage("Image is not clean");

        }

        private async Task<bool> UriIsValid(string uri, CancellationToken cancellationToken)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg", ".av1", ".avif" };

            try
            {
                // Create a Uri object from the string
                Uri uriResult;
                if (!Uri.TryCreate(uri, UriKind.Absolute, out uriResult))
                {
                    // If the URI is invalid, return false
                    return false;
                }

                // Get the path from the URI and extract the file extension
                string extension = System.IO.Path.GetExtension(uriResult.AbsolutePath).ToLower();

                // Check if the extension is in the list of allowed extensions
                return allowedExtensions.Contains(extension);
            }
            catch (Exception)
            {
                // In case of any error, return false
                return false;
            }
        }

        private async Task<bool> IsImageClean(string uri, CancellationToken cancellationToken)
        {
            bool isClean = true;
            bool isHealthy = true;
            bool isNude = true;
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage imageData = await httpClient.GetAsync(uri,cancellationToken);
                byte[] image =  await imageData.Content.ReadAsByteArrayAsync();
                using (var ms = new MemoryStream(image))
                {
                    try
                    {
                        var img = Image.FromStream(ms);
                    }
                    catch
                    {
                        isClean = false;
                    }
                }
                isHealthy = await IsHealthy(image);
                isNude = await IsNudeOrX(image);
                return isHealthy & isClean & isNude;
            }
        }

        private async Task<bool> IsHealthy(byte[] imageData)
        {
            //var ms = new MemoryStream(imageData);
            //byte[] fileBytes = ms.ToArray();
            try
            {
                var clamClient = new ClamClient("localhost", 3310);
                var scanResult = await clamClient.SendAndScanFileAsync(imageData);
                if (scanResult.Result == ClamScanResults.VirusDetected)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                var msg=ex.Message;
                return false;
            }
        }

        private async Task<bool> IsNudeOrX(byte[] imageData)
        {

            try
            {
                // Download the image from the provided URI
                var nsfwSpy = new NsfwSpy();
                var result = nsfwSpy.ClassifyImage(imageData);

                return !result.IsNsfw;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., network errors, invalid images, etc.)
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        private async Task<bool> NameAlreadyTaken(string nameImage, CancellationToken cancellationToken)
        {
            return !await _context.Images
                .AnyAsync(t => t.NomImage == nameImage, cancellationToken);
        }
    }
}
