using Application.Common.Interfaces;
using Application.Common.Methods;
using Application.Common.Models;
using Domain.Entities;
using Domain.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserCases.UserLambda.Command
{

    public record UseCase_CreationUser(string email, string username, string password, string Location, DateTime BirthDate, string phoneNumber, string description) : IRequest<Result>;

    public class UseCase_CreationUserHandler : IRequestHandler<UseCase_CreationUser, Result>
    {
        private readonly IApplicationDbContext _context;
        private const string connection = "Username-Password-Authentication";
        private readonly IAuth0Client<UserCreate> _client;
        private readonly int idRole = 2;
        private readonly int idForfait = 1;




        public UseCase_CreationUserHandler(IApplicationDbContext context, IAuth0Client<UserCreate> auth0Client)
        {
            _context = context;
            _client = auth0Client;
        }


        public async Task<Result> Handle(UseCase_CreationUser request, CancellationToken cancellationToken)
        {
            var result = 0;
            var user_id = request.username + "_" + Guid.NewGuid();
            var role = await getRole(idRole);
            try
            {
                await _context.beginTransactionAsync();
                await process(role,user_id, request.email, request.username, request.password, request.Location, request.BirthDate, request.phoneNumber, request.description);
                result = await _context.SaveChangesAsync(cancellationToken);
                await _context.commitTransactionAsync();

            }
            catch (Exception ex)
            {
                await _context.rollbackTransactionAsync();
                await userAuthDelete(user_id);
                return ManageResult.result(result, "Creation_User", null);
            }
            finally
            {
                await _context.Dispose();
            }

            return ManageResult.result(result, "Creation_User", null);
        }

        private async Task process(Roles role, string user_id, string email, string username, string password, string Location, DateTime BirthDate, string phoneNumber, string description)
        {
            await userAuthCreation(user_id,email,username,password);
            await defaultRoleAuth(role,user_id);
            await creationUser(user_id,email, username, password,Location, BirthDate, phoneNumber,description);
            await defaultForfait(user_id);
            await defaultRole(user_id);
            await libraryCreation(user_id);
        }

        private async Task creationUser(string user_id, string email, string username, string password, string Location, DateTime BirthDate, string phoneNumber, string description)
        {
            var entity = new User()
            {
                user_id = user_id,
                email = email,
                username = username,
                Location = Location,
                password = password,
                BirthDate = BirthDate,
                phoneNumber = phoneNumber,
                description = description,
                avatar = ""
            };
            await _context.Users.AddAsync(entity);
        }
        private async Task userAuthCreation(string user_id, string email, string username, string password)
        {
            var entityAuth = new UserCreate()
            {
                user_id = user_id,
                username = username,
                email = email,
                password = password,
                connection = connection
            };
            await _client.CreateUserAsync(entityAuth);
        }

        private async Task defaultForfait(string user_id)
        {
            var newForfaitsUser = new Forfait_UserIntern()
            {
                IdForfait = idForfait,
                user_id = user_id,
            };
            await _context.Forfait_Users.AddAsync(newForfaitsUser);
        }

        private async Task defaultRole(string user_id)
        {
            var role_User = new Roles_Users()
            {
                idRole = idRole,
                user_id = user_id
            };
           await _context.Roles_Users.AddAsync(role_User);
        }

        private async Task defaultRoleAuth(Roles role,string user_id)
        {
            await _client.AddressingRole(user_id, role.AuthRoleId);
        }

        private async Task libraryCreation(string user_id)
        {
            var entity = new Library()
            {
                IdLibrary = "Library_" + user_id,
                NameLibrary = "Library of " + user_id,
                user_id = user_id,

            };
            await _context.Libraries.AddAsync(entity);
        }


        private async Task<Roles> getRole(int idRole)
        {
            var role = await _context.Roles.FindAsync(idRole);
            return role;
        }


        private async Task userAuthDelete(string user_id)
        {
            await _client.DeleteUserAsync(user_id);
        }

    }
}
