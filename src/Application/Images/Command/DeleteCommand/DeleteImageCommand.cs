using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Images.Command.DeleteCommand
{
    public record DeleteImageCommand(int id, string user_id) : IRequest<Result>;

    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        public DeleteImageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Images
           .Where(l => l.IdImage == request.id)
           .SingleOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(nameof(Images), request.id);

            var result = await deleteProcess(request.user_id, request.id, cancellationToken);
            if (!result.Succeeded) throw new InvalidOperationsException("Les chapitres n'ont pas pu etre supprimé",result);
            _context.Images.Remove(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);


            if (resultTask > 0)
            {
                File.Delete(entity.PathImage);
                return Result.Success("Image deleted with success");
            }
            return Result.Failure("Action Failed : image could not be deleted", new List<string>()); //implemnt errors with the chapter to delete
        }

        private async Task<Result> deleteProcess(string user_id, int idImage, CancellationToken cancellationToken)
        {
            var chapters = await getChaptersRelated(user_id, idImage);
            _context.Chapitres.RemoveRange(chapters);
            foreach(var chapter in chapters)
            {
                var story=await _context.Stories.FindAsync(chapter.IdStory);
                _context.Stories.Remove(story);
            }
            var resultTask = await _context.SaveChangesAsync(cancellationToken);
             return Result.Success($" delete process with success:  {resultTask} chapters deleted ");
        }
        private async Task<List<Chapitre>> getChaptersRelated(string user_id,int idImage)
        {
            var user=await _context.userEntities
                .Where(t=>t.user_id==user_id)
                .SingleOrDefaultAsync();
            var books=await _context.StoryTellings
                .Where(t=>t.IdUser == user.IdUser)
                .ToListAsync();
            var ids = new List<int>();
            foreach(var book in books)
            {
                ids.Add(book.IdStoryTelling);
            }
            return await  _context.Chapitres
                .Where(t=>ids.Contains(t.IdStoryTelling))
                .ToListAsync();
        }
    }

}
