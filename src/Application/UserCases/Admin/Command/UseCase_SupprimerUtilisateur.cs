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

namespace Application.UserCases.Admin.Command
{

    public record UseCase_SupprimerUtilisateur(string user_id) : IRequest<Result>;

    public class UseCase_AcheterLivreHandler : IRequestHandler<UseCase_SupprimerUtilisateur, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        private readonly int _roleAuthor=3;

        public UseCase_AcheterLivreHandler(IApplicationDbContext context, IUser user)
        {
            _context = context;
            _user = user;
        }


        public async Task<Result> Handle(UseCase_SupprimerUtilisateur request, CancellationToken cancellationToken)
        {
            var result = 0;
            var admin = _user.getUserId();
            try
            {
                await _context.beginTransactionAsync();
                await process(request.user_id,admin);
                result = await _context.SaveChangesAsync(cancellationToken);
                await _context.commitTransactionAsync();

            }
            catch (Exception ex)
            {
                await _context.rollbackTransactionAsync();
                return ManageResult.result(result, "Supprime_User", null);
            }
            finally
            {
                await _context.Dispose();
            }

            return ManageResult.result(result, "Supprime_User", null);
        }

        private async Task process(string user_id,string admin)
        {
            await checkUserData(user_id,admin);
            await supprimeUser(user_id);
        }

        private async Task supprimeUser(string user_id)
        {
            var user=await  _context.Users.FindAsync(user_id);
            _context.Users.Remove(user);
        }

        private async Task checkUserData(string user_id,string admin)
        {
            await checkUserLambda(user_id);
        }

        private async Task checkUserLambda(string user_id)
        {
            var library=await _context.Libraries.SingleOrDefaultAsync(t=>t.user_id==user_id);
            var books = await _context.storyTellBoxes.Where(t => t.IdLibrary == library.IdLibrary).ToListAsync();
            if(books.Count!=0)
            _context.storyTellBoxes.RemoveRange(books);
        }

        private async Task changeOwner(List<StoryTelling> stories,string admin)
        {
            foreach (var story in stories)
            {
                story.user_id = admin;
            }
        }
    }
}
