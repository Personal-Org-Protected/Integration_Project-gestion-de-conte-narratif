using Application.Common.Exceptions;
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

namespace Application.UserCases.UserLambda.Command
{

    public record UseCase_AcheterLivre(int storyTell, string NameBook,double price) : IRequest<Result>;

    public class UseCase_AcheterLivreHandler : IRequestHandler<UseCase_AcheterLivre, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;


        public UseCase_AcheterLivreHandler(IApplicationDbContext context, IUser user)
        {
            _context = context;
            _user = user;
        }


        public async Task<Result> Handle(UseCase_AcheterLivre request, CancellationToken cancellationToken)
        {
            var result = 0;
            var user_id = _user.getUserId();
            try
            {
                await _context.beginTransactionAsync();
                await process(user_id,request.storyTell,request.price,request.NameBook);
                result = await _context.SaveChangesAsync(cancellationToken);
                await _context.commitTransactionAsync();

            }
            catch (Exception ex)
            {
                await _context.rollbackTransactionAsync();
                return ManageResult.result(result, "Achat_Livre", null);

            }
            finally
            {
                await _context.Dispose();
            }

            return ManageResult.result(result, "Achat_Livre", null);
        }

        private async Task process(string user_id,int idStoryTell,double price,string name)
        {
            await createStoryBox(user_id, idStoryTell);
            await createTransaction(user_id, idStoryTell, name, price);
            await updateNumberRef(idStoryTell);
        }


        private async Task createStoryBox(string user_id, int idStoryTell)
        {
            var library = await _context.Libraries
         .Where(t => t.user_id == user_id)
         .FirstOrDefaultAsync() ?? throw new NotFoundException();

            var entity = new StoryTellBox()
            {
                IdStoryTell = idStoryTell,
                lastPageChecked = 1,
                IdLibrary = library.IdLibrary
            };
            await _context.storyTellBoxes.AddAsync(entity);
        }

        private async Task createTransaction(string user_id, int idStoryTell,string nameBook,double price)
        {
            var entity = new Transaction()
            {
                NameBook = nameBook,
                price = price,
                DateTransaction = DateTime.Now,
                user_id = user_id,
                StoryTellId = idStoryTell,
            };

            await _context.Transactions.AddAsync(entity);
        }


        private async Task updateNumberRef(int idStoryTell)
        {
            var histoire = await _context.StoryTellings
             .FindAsync(idStoryTell)
             ?? throw new NotFoundException();

            histoire.numberRef++;
            _context.StoryTellings.Update(histoire);
        }
    }
}
