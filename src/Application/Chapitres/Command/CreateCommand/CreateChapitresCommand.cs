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

namespace Application.Chapitres.Command.CreateCommand
{
    public class CreateChapitresCommand:IRequest<Result>
    {
        public int IdStoryTelling { get; set; }
        public int IdImage { get; set; }
        public int IdStory { get; set; }
        public int Order { get; set; }
    }

    public class CreateChapitresCommandHandler : IRequestHandler<CreateChapitresCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateChapitresCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(CreateChapitresCommand request, CancellationToken cancellationToken)
        {

            var entity = new Chapitre
            {
                IdImage = request.IdImage,
                IdStory = request.IdStory,
                IdStoryTelling = request.IdStoryTelling,
                Order = await GetLastIndex(request.IdStoryTelling,request.Order),
            };

            _context.Chapitres.Add(entity);
            var resultTask=await _context.SaveChangesAsync(cancellationToken);

            if (resultTask > 0) return Result.Success("Chapitre added with success");
            return Result.Failure("Action Failed : Chapitre could not be added", new List<string>());
        }

        private async Task<int> GetLastIndex(int storyTell,int order)
        {
            var chapters = await _context.Chapitres
                .Where(t => t.IdStoryTelling == storyTell)
                .ToListAsync();
            if (order!=0&&!OrderAlredayPicked(chapters,order)) return order;
            return chapters.Count+1;
        }
        private bool OrderAlredayPicked(List<Chapitre> chapitres,int order)
        {
            return chapitres.Any(t => t.Order == order);
        } 


    }
}
