using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Domain.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Command.UpdateCommand
{
    public class UpdateUserCommand : IRequest<Result>
    {
        public string user_id { get; set; }
        public string username { get; set; }
        public string Location { get; set; }
        public string phoneNumber { get; set; }

    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAuth0Client<UserUpdate> _client;

        public UpdateUserCommandHandler(IApplicationDbContext context, IAuth0Client<UserUpdate> auth0Client)
        {
            _context = context;
            _client = auth0Client;
        }
        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _context.beginTransaction();
            var Entity = await _context.Users
                .FirstOrDefaultAsync(t => t.user_id == request.user_id) ?? throw new NotFoundException(); 


            
            var authResult=await updateUserAuth(request.user_id,request.username);
            var identityResult = await updateUserIdentity(request,Entity,cancellationToken);

            if (await onCheckState(authResult,identityResult,request.user_id,Entity.username)) { await _context.commitTransaction(); return Result.Success("User modified with success"); }
            return Result.Failure("Action Failed : User could not be Modified", new List<string>());
        }


        private async Task<bool> onCheckState(bool auth, int identity, string user_id,string username)
        {
            if (!auth && identity!=1)
            {
                await _context.rollbackTransaction();
                await CancelUpdateAuth(user_id, username);
                return false;
            }

            return true;
        }
        private async Task<bool> updateUserAuth(string user_id, string userName)
        {
            var userAuth = new UserUpdate()
            {
                username = userName,
            };

            var result= await _client.UpdateUserAsync(userAuth, user_id);
            return result.Succeeded;
        }
        private async Task<int> updateUserIdentity(UpdateUserCommand request,User user,CancellationToken cancellationToken)
        {

            user.Location = request.Location;
            user.username = request.username;
            user.phoneNumber = request.phoneNumber;
            _context.Users.Update(user);
            return  await _context.SaveChangesAsync(cancellationToken);
        }


        private async Task CancelUpdateAuth(string user_id,string username)
        {
            var userAuth = new UserUpdate()
            {
                username = username,
            };
            await _client.UpdateUserAsync(userAuth, user_id);

        }



    }
}
