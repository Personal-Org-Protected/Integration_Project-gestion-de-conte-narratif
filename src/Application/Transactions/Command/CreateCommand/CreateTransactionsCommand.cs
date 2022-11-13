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

namespace Application.Transactions.Command.CreateCommand
{
    public class CreateTransactionsCommand: IRequest<Result>
    {
        public string NameBook { get; set; }
        public double price { get; set; }
        public string User_id { get; set; }
        public int StoryTellId { get; set; }
        //public string IdLibrary { get; set; }
    }

    public class CreateTransactionsCommandHandler : IRequestHandler<CreateTransactionsCommand, Result>
    {
        private readonly IApplicationDbContext _context;

        public CreateTransactionsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result> Handle(CreateTransactionsCommand request, CancellationToken cancellationToken)
        {
            await _context.beginTransaction();

            //verufy if thte user has a forfeit
            var user = await _context.userEntities
                .Where(t => t.user_id == request.User_id)
                .FirstOrDefaultAsync()?? throw new NotFoundException(); 

            var forfait = await verifyForfeit(user);

            return await CreateEntity(request,user ,forfait, cancellationToken);


        }

        private  async Task<Result> TransactionSuccess(string idUser,int idStoryTell, CancellationToken cancellationToken)
        {
            var library=await _context.Libraries
                .Where(t=>t.IdUser == idUser)
                .FirstOrDefaultAsync() ?? throw new NotFoundException();
            var entity = new StoryTellBox()
            {
                IdStoryTell = idStoryTell,
                lastPageChecked = 0,
                IdLibrary= library.IdLibrary
            };
            _context.storyTellBoxes.Add(entity);
            var resulTask = await _context.SaveChangesAsync(cancellationToken);
            if(resulTask > 0) { return Result.Success("StoryBox added with success", entity.IdBox); }
            return Result.Failure("Action Failed : StoryBox could not be added", new List<string>());


        }

        private async Task<ForfaitClient> verifyForfeit(UserEntity userEntity)
        {
            var forfaitUserList=await _context.Forfait_Users
                .Where(t=>t.IdUser==userEntity.IdUser)
                .ToListAsync();

            var idS=new List<int>();
            foreach(var item in forfaitUserList)
            {
                idS.Add(item.IdForfait);
            }

            return await AppropriateForfeit(idS);
        }
        private async Task<ForfaitClient> AppropriateForfeit(List<int> idS)
        {
            var forfait = await _context.Forfaits
                .Where(t => idS.Contains(t.IdForfait) && !t.IsForAuthor)
                .ToListAsync();
            if (forfait.Any(t => t.RoleId == 2)) return forfait.SingleOrDefault(t => t.RoleId == 2);
            return forfait.SingleOrDefault();
        }

        private async Task<Result> CreateEntity(CreateTransactionsCommand request,UserEntity user,ForfaitClient forfait, CancellationToken cancellationToken)
        {
            var entity = new Transaction()
            {
                NameBook = request.NameBook,
                price = PriceOperations(forfait.Reduction, request.price),
                DateTransaction = DateTime.Now,
                User_id = user.IdUser,
                StoryTellId = request.StoryTellId,
            };

            _context.Transactions.Add(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);
            var resultAfterTask = await TransactionSuccess(user.IdUser, request.StoryTellId, cancellationToken);

           return  await ResultTransaction(resultTask,resultAfterTask,entity);
        }

        private async Task<Result> ResultTransaction(int resultTask, Result resultAfterTask,Transaction entity)
        {
            if (resultTask > 0 && resultAfterTask.Succeeded)
            {
                await _context.commitTransaction();
                return Result.Success("Transaction added with success", entity.TransactionId);
            }

            await _context.rollbackTransaction();
            return Result.Failure("Action Failed : Transaction could not be added", new List<string>());
        }

        private double PriceOperations(double reduction, double price)
        {
            return price-(price*reduction)/100;
        }
    }
}
