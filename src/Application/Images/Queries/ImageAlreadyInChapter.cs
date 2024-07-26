using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Images.Queries.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Images.Queries
{
    public record ImageAlreadyInChapter(int idImage):IRequest<AlreadyInChapter>;
    public class ImageAlreadyInChapteraHandler:IRequestHandler<ImageAlreadyInChapter, AlreadyInChapter>
    {
        private readonly IApplicationDbContext _context;

        public ImageAlreadyInChapteraHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AlreadyInChapter> Handle(ImageAlreadyInChapter request, CancellationToken cancellationToken)
        {
            var result = new AlreadyInChapter();
            var chapters = await _context.Chapitres
                .Where(t => t.IdImage == request.idImage)
                .FirstOrDefaultAsync();
            if(chapters != null) { result.IsAlreadyInChapter = true; return result; }
            return result;
        }
    }

}
