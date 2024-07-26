using Application.Common.Interfaces;
using Application.Common.Methods;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserCases.Author.Command
{
    public record UseCase_CreationHistoire(string url,string NameStory, double price, string synopsis, int idTag) : IRequest<Result>;


    public class UseCase_CreationHistoireHandlder : IRequestHandler<UseCase_CreationHistoire, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public UseCase_CreationHistoireHandlder(IApplicationDbContext context, IUser user )
        {
            _context = context;
            _user = user;
        }

        public async Task<Result> Handle(UseCase_CreationHistoire request, CancellationToken cancellationToken)
        {
            var result = 0;
            var user_id = _user.getUserId();
            try
            {
                await _context.beginTransactionAsync();
                await process(user_id,request.url, request.NameStory, request.price, request.synopsis, request.idTag);
                result = await _context.SaveChangesAsync(cancellationToken);
                await _context.commitTransactionAsync();

            }
            catch (Exception ex)
            {
                await _context.rollbackTransactionAsync();
                return ManageResult.result(result, "Create_Story", null);

            }
            finally
            {
                await _context.Dispose();
            }

            return ManageResult.result(result, "Create_Story", null);
        }

        private async Task process(string user_id, string url, string NameStory, double price, string synopsis, int idTag)
        {
            var zoneId= await CreateZoneComm();
            await createHistoire(user_id,url,NameStory,price,synopsis,idTag,zoneId);
            await changeTagCount(idTag);
        }

        private async Task createHistoire(string user_id,string url, string NameStory, double price, string synopsis, int idTag,int zoneId)
        {
            var entity = new StoryTelling()
            {
                NameStory = NameStory,
                url = url,
                user_id = user_id,
                DateCreation = DateTime.Now,
                Sypnopsis = synopsis,
                IdZone = zoneId,
                idTag = idTag,
                rating = 0
            };
            await _context.StoryTellings.AddAsync(entity);
        }

        private async Task changeTagCount(int idTag)
        {
            var tag = await _context.Tag
                .FindAsync(idTag);
            tag.numberRef++;
            _context.Tag.Update(tag);
        }

        private async Task<int> CreateZoneComm()
        {
            var ZoneComm = await _context.ZoneComments.AddAsync(new ZoneCommentary());
            return ZoneComm.Entity.IdZone;
        }
    }
}
