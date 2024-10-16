﻿using Application.Common.Exceptions;
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
        public int StoryTellId { get; set; }
        //public string IdLibrary { get; set; }
    }

    public class CreateTransactionsCommandHandler : IRequestHandler<CreateTransactionsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public CreateTransactionsCommandHandler(IApplicationDbContext context,IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<Result> Handle(CreateTransactionsCommand request, CancellationToken cancellationToken)
        {
            var user_id=_user.getUserId();

            //verufy if thte user has a forfeit
            var entity = new Transaction()
            {
                NameBook = request.NameBook,
                price = request.price,
                DateTransaction = DateTime.Now,
                user_id = user_id,
                StoryTellId = request.StoryTellId,
            };

            await _context.Transactions.AddAsync(entity);
            var resultTask = await _context.SaveChangesAsync(cancellationToken);
            //var forfait = await verifyForfeit(user);

            if (resultTask > 0) { return Result.Success("Transaction operate with success", entity.TransactionId); }
            return Result.Failure("Action Failed : Transaction failed ", new List<string>());


        }

        private  async Task<Result> TransactionSuccess(string user_id,int idStoryTell, CancellationToken cancellationToken)
        {
            var library=await _context.Libraries
                .Where(t=>t.user_id == user_id)
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

        //private async Task<ForfaitClient> verifyForfeit(UserEntity userEntity)
        //{
        //    var forfaitUserList=await _context.Forfait_Users
        //        .Where(t=>t.IdUser==userEntity.IdUser)
        //        .ToListAsync();

        //    var idS=new List<int>();
        //    foreach(var item in forfaitUserList)
        //    {
        //        idS.Add(item.IdForfait);
        //    }

        //    return await AppropriateForfeit(idS);
        //}
        //private async Task<ForfaitClient> AppropriateForfeit(List<int> idS)
        //{
        //    var forfait = await _context.Forfaits
        //        .Where(t => idS.Contains(t.IdForfait) && !t.IsForAuthor)
        //        .ToListAsync();
        //    if (forfait.Any(t => t.RoleId == 2)) return forfait.SingleOrDefault(t => t.RoleId == 2);
        //    return forfait.SingleOrDefault();
        //}

        //private async Task<Result> CreateEntity(CreateTransactionsCommand request,UserEntity user,ForfaitClient forfait, CancellationToken cancellationToken)
        //{
        //    var entity = new Transaction()
        //    {
        //        NameBook = request.NameBook,
        //        price = PriceOperations(forfait.Reduction, request.price),
        //        DateTransaction = DateTime.Now,
        //        User_id = user.IdUser,
        //        StoryTellId = request.StoryTellId,
        //    };

        //    _context.Transactions.Add(entity);
        //    var resultTask = await _context.SaveChangesAsync(cancellationToken);
        //    var resultAfterTask = await TransactionSuccess(user.IdUser, request.StoryTellId, cancellationToken);

        //   return  await ResultTransaction(resultTask,resultAfterTask,entity);
        //}

        //private async Task<Result> ResultTransaction(int resultTask, Result resultAfterTask,Transaction entity)
        //{
        //    if (resultTask > 0 && resultAfterTask.Succeeded)
        //    {
        //        await _context.commitTransaction();
        //        return Result.Success("Transaction added with success", entity.TransactionId);
        //    }

        //    await _context.rollbackTransaction();
        //    return Result.Failure("Action Failed : Transaction could not be added", new List<string>());
        //}

        //private double PriceOperations(double reduction, double price)
        //{
        //    return price-(price*reduction)/100;
        //}
    }
}
