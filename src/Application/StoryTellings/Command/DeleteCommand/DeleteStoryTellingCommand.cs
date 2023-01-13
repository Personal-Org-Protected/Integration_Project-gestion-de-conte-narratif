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

namespace Application.StoryTellings.Command.DeleteCommand
{
    public record DeleteStoryTellingCommand(int id) :IRequest<Result>;

    public class DeleteStoryTellingCommandHandler : IRequestHandler<DeleteStoryTellingCommand, Result>
    {

        private readonly IApplicationDbContext _context;

        public DeleteStoryTellingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(DeleteStoryTellingCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.StoryTellings
            .Where(l => l.IdStoryTelling == request.id)
            .SingleOrDefaultAsync(cancellationToken)?? throw new NotFoundException(nameof(StoryTelling), request.id);
           //var resultChapter= await deleteProcess(request.id, cancellationToken); ; if (resultChapter == null) return resultChapter;

            _context.StoryTellings.Remove(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);


            if (resultTask > 0) return Result.Success("Story deleted with success");
            return Result.Failure("Action Failed : Story could not be deleted", new List<string>());
        }


        //private async Task<Result> deleteProcess(int bookId, CancellationToken cancellationToken)
        //{
        //    var chapters = await getChaptersRelated(bookId);
        //    _context.Chapitres.RemoveRange(chapters);
        //    foreach (var chapter in chapters)
        //    {
        //        var story = await _context.Stories
        //            .FindAsync(chapter.IdStory)?? throw new NotFoundException($"story{chapter.IdStory} not found");

        //        _context.Stories.Remove(story);
        //    }
        //    var resultTask = await _context.SaveChangesAsync(cancellationToken);
        //     return Result.Success(" delete process with success : " +resultTask/2+" chapter deleted");

        //}
        //private async Task<List<Chapitre>> getChaptersRelated(int bookId)
        //{
        //   return  await _context.Chapitres
        //     .Where(t => t.IdStoryTelling == bookId)
        //     .ToListAsync();
        //}
    }

}
