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

namespace Application.Users.Command.CreateCommand
{

    public class CreateUserCommand : IRequest<Result>
    {
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Location { get; set; }
        public DateTime BirthDate { get; set; }
        public string phoneNumber { get; set; }

    }

    public class CreateUserCommandResponseHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IAuth0Client<UserCreate> _client;
        private readonly IApplicationDbContext _context;
        private const string connection = "Username-Password-Authentication"; 

        public CreateUserCommandResponseHandler(IApplicationDbContext context,IAuth0Client<UserCreate> auth0Client)
        {
            _context = context;
            _client = auth0Client;
        }
        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _context.beginTransaction();
            var role = await _context.Roles.FindAsync(1);
            var idUser = request.username + "_" + Guid.NewGuid();
            var AuthResult=await createUserAuth0(idUser,request,role);
            var IdentityResult = await createUserIdentity(request, idUser, cancellationToken);
            var EntityResult=await createUserEntity(idUser,cancellationToken);
            var RoleConfigResult = await addressRole(idUser, role, cancellationToken);

            if (await OnStateCheck(AuthResult, IdentityResult, EntityResult,idUser) && RoleConfigResult) { await _context.commitTransaction(); return Result.Success("User added with success"); }
            return Result.Failure("Action Failed : User could not be added", new List<string>());
        }


        private async Task<int> createUserIdentity(CreateUserCommand request,string idUser,CancellationToken cancellationToken)
        {
            var entity = new User()
            {
                user_id = idUser,
                email = request.email,
                username = request.username,
                Location = request.Location,
                password = request.password,
                BirthDate = request.BirthDate,
                phoneNumber = request.phoneNumber,
            };
            _context.Users.Add(entity);
            return await _context.SaveChangesAsync(cancellationToken);
            
        }
        private async Task<bool> createUserAuth0(string idUser, CreateUserCommand request, Roles role)
        {
            var entityAuth = new UserCreate()
            {
                user_id = idUser,
                username = request.username,
                email = request.email,
                password = request.password,
                connection = connection
            };
            var result = await _client.CreateUserAsync(entityAuth);
            var result_role=await roleConfigAuth(idUser, role);


            return result.Succeeded && result_role;
        }
        private async Task<bool> roleConfigAuth(string user_id,Roles role)
        {
            var result= await _client.AddressingRole(user_id, role.AuthRoleId);
            return result.Succeeded;
        }

        private async Task<int> roleConfigIdentity(string user_id, Roles role, CancellationToken cancellationToken)
        {
            var resultRoleUser = new Roles_Users()
            {
                idRole = role.IdRole,
                user_id = user_id,
            };
            _context.Roles_Users.Add(resultRoleUser);
            var result= await _context.SaveChangesAsync(cancellationToken);
            return result;
        }
        private async Task<int> roleConfigEntity(string user_id, Roles role, CancellationToken cancellationToken)
        {
            var idUser = "Intern_User_" + user_id;
            var forfait = await _context.Forfaits
                        .Where(t => t.RoleId == role.IdRole && t.ForfaitLibelle=="Default")
                        .FirstOrDefaultAsync();
            var ForfaitUser = new Forfait_UserIntern()
            {
                IdForfait = forfait.IdForfait,
                IdUser = idUser,
            };
            _context.Forfait_Users.Add(ForfaitUser);
           return await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task<bool> addressRole(string user_id, Roles role, CancellationToken cancellationToken)
        {
            var resultIdentity=await roleConfigIdentity(user_id,role,cancellationToken);
            var resultEntity = await roleConfigEntity(user_id,role,cancellationToken);
            if (resultIdentity>0 && resultEntity>0)return true;

            return false;
        }
        private async Task<int> createUserEntity(string idUser, CancellationToken cancellationToken)
        {
            var entity = new UserEntity()
            {
                IdUser= "Intern_User_"+idUser,
                user_id = idUser,
            };
            _context.userEntities.Add(entity);
            var result= await _context.SaveChangesAsync(cancellationToken);
            return result;
        }

        private async Task<bool> OnStateCheck(bool auth,int identity,int entity,string userAuth_id)
        {
            if (auth && identity == 1 && entity == 1)
            {
              return true;
            }
            await DeleteAuhtUser(userAuth_id);
            await _context.rollbackTransaction();
            return false;

        }
        //private async Task DeleteIdentityUser(string idUser,CancellationToken cancellationToken)
        //{
        //    var user=await _context.Users.FindAsync(idUser);
        //     _context.Users.Remove(user);
        //    await _context.SaveChangesAsync(cancellationToken);
        //}



        private async Task DeleteAuhtUser(string idUser)
        {
            await _client.DeleteUserAsync(idUser);

        }
        //private async Task DeleteEntityUser(string idUser, CancellationToken cancellationToken)
        //{
        //    var user = await _context.userEntities.FirstOrDefaultAsync(t=>t.user_id==idUser);
        //    _context.userEntities.Remove(user);
        //    await _context.SaveChangesAsync(cancellationToken);

        //}
    }
}
