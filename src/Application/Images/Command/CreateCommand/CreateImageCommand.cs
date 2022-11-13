using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Images.Command.CreateCommand
{
    public class CreateImageCommand : IRequest<Result> 
    {
        public string NomImage { get; set; }
        public string descriptionImage { get; set; }
        public int IdTag { get; set; }
        public string Uri { get; set; }
        public string user_id { get; set; }

    }

    public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateImageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            var format = request.Uri[request.Uri.LastIndexOf('.')..];
            var entity = new Image()
            {
                NomImage = request.NomImage,
                descriptionImage=request.descriptionImage,
                PathImage=CreateImageFile.PathImage(request.NomImage, CreateImageFile.GetFormat(format)),
                IdTag= await getDefaultTag(request.IdTag),
                Uri=request.Uri,
                DateCreation=DateTime.Now,
                owner=await getUser(request.user_id)
            };
            _context.Images.Add(entity);
            var resultTask=await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) {
                CreateImageFile.EnsureImageCreated(request.Uri,request.NomImage,CreateImageFile.GetFormat(format));
                return Result.Success("Image added with success"); }
            return Result.Failure("Action Failed : image could not be added", new List<string>());
        }

        private async Task<int> getDefaultTag(int idTag)
        {
            if (idTag == 0) { var result = await _context.Tag
                     .Where(t => t.NameTag == "Default")
                     .SingleOrDefaultAsync() ?? throw new NotFoundException("no tag by default found"); idTag = result.IdTag;
            }
            return idTag;
        }
        private async Task<string> getUser(string user_id)
        {
            var user = await _context.userEntities
                .Where(t => t.user_id == user_id)
                .SingleOrDefaultAsync()?? throw new NotFoundException("No user found in image creation");
            return user.IdUser;
        }
    }


    internal static class CreateImageFile
    {
        private static string PathFolder= @"D:/fichTelechargement/Images";
        internal static void EnsureImageCreated(string UriString,string NomImage,string format)
        {
            if (!Directory.Exists(PathFolder))
                Directory.CreateDirectory(PathFolder);

            using (WebClient client = new())
            {
                client.DownloadFile(new Uri(UriString), PathImage(NomImage,format));
            }
        }


        internal static string GetFormat(string formatBrut)
        {
            var FormatType = new string[]
            {
                "jpeg","jpg","png","svg","bmp","gif","webp"
            };
            
            foreach (var format in FormatType)
            {
                if (formatBrut.Contains(format))
                {
                   //var formatNet=formatBrut.Substring(formatBrut.IndexOf('.'),format.Length+1);
                    return ".webp";
                };
            }
            throw new Exception("Format of image not valid");
        }
        internal static string PathImage(string nomImage, string format)
        {
            return PathFolder + "/" + nomImage + format;
        }
 
    }
}
