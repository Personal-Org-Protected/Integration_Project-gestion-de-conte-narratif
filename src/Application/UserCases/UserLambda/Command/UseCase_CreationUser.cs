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
        private readonly int idRole = 2;
        private readonly int idForfait = 1;




        public UseCase_CreationUserHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Result> Handle(UseCase_CreationUser request, CancellationToken cancellationToken)
        {
            var result = 0;
            var user_id = request.username + "_" + Guid.NewGuid();
            try
            {
                await _context.beginTransactionAsync();
                await process(user_id, request.email, request.username, request.password, request.Location, request.BirthDate, request.phoneNumber, request.description);
                result = await _context.SaveChangesAsync(cancellationToken);
                await _context.commitTransactionAsync();

            }
            catch (Exception ex)
            {
                await _context.rollbackTransactionAsync();
                return ManageResult.result(result, "Creation_User", null);
            }
            finally
            {
                await _context.Dispose();
            }

            return ManageResult.result(result, "Creation_User", null);
        }

        private async Task process(string user_id, string email, string username, string password, string Location, DateTime BirthDate, string phoneNumber, string description)
        {
            await creationUser(user_id,email, username, password,Location, BirthDate, phoneNumber,description);
            await libraryCreation(user_id);
        }

        private async Task creationUser(string user_id, string email, string username, string password, string Location, DateTime BirthDate, string phoneNumber, string description)
        {
            var entity = new User()
            {
                user_id = user_id,
                email = email,
                username = username,
                avatar = ""
            };
            await _context.Users.AddAsync(entity);
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



    }
}
