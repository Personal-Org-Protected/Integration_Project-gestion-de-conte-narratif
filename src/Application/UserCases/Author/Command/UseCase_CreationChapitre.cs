using Application.Common.Interfaces;
using Application.Common.Methods;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserCases.Author.Command
{
    public record UseCase_CreationChapitre(int IdStoryTelling, int IdImage, int Order, string title, string text) : IRequest<Result>;


    public class UseCase_CreationChapitreHandlder : IRequestHandler<UseCase_CreationChapitre, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public UseCase_CreationChapitreHandlder(IApplicationDbContext context, IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<Result> Handle(UseCase_CreationChapitre request, CancellationToken cancellationToken)
        {
            var result = 0;
            var user_id = _user.getUserId();
            try
            {
                await _context.beginTransactionAsync();
                await process(request.IdImage, request.IdStoryTelling, request.Order,request.title,request.text);
                result = await _context.SaveChangesAsync(cancellationToken);
                await _context.commitTransactionAsync();

            }
            catch (Exception ex)
            {
                await _context.rollbackTransactionAsync();
                return ManageResult.result(result, "Create_Chapter", null);
            }
            finally
            {
                await _context.Dispose();
            }

            return ManageResult.result(result, "Create_Chapter", null);
        }

        public async Task process(int IdImage, int IdStoryTelling, int Order,string title,string text)
        {
            var storyId=await creationNarration(title,text);
            await creationChapter(IdImage, storyId, IdStoryTelling, Order);
        }

        public async Task creationChapter(int IdImage, int IdStory, int IdStoryTelling, int Order)
        {
            var lastIndex = await GetLastIndex(IdStoryTelling, Order);

            var entity = new Chapitre
            {
                IdImage = IdImage,
                IdStory = IdStory,
                IdStoryTelling = IdStoryTelling,
                Order = lastIndex,
            };

            _context.Chapitres.Add(entity);
        }

        private async Task<int> GetLastIndex(int storyTell, int order)
        {
            var chapters = await _context.Chapitres
                .Where(t => t.IdStoryTelling == storyTell)
                .ToListAsync();
            if (order != 0 && !OrderAlredayPicked(chapters, order)) return order;
            return chapters.Count + 1;
        }
        private bool OrderAlredayPicked(List<Chapitre> chapitres, int order)
        {
            return chapitres.Any(t => t.Order == order);
        }

        private async Task<int> creationNarration(string name,string text)
        {
            var entity = new Story
            {
                DateCreation = DateTime.Now,
                NomStory = name,
                TextStory = text
            };

            var story=_context.Stories.Add(entity);
            return story.Entity.IdStory;
        }

    }
}
