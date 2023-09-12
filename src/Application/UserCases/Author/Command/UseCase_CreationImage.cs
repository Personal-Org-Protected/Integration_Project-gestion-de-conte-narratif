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
    public record UseCase_CreationImage(string NomImage, string descriptionImage, int IdTag, string Uri) :IRequest<Result>;


    public class UseCase_CreationImageHandlder : IRequestHandler<UseCase_CreationImage, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public UseCase_CreationImageHandlder(IApplicationDbContext context, IUser user)
        {
            _context = context;
            _user = user;
        }
        public async Task<Result> Handle(UseCase_CreationImage request, CancellationToken cancellationToken)
        {
            var result = 0;
            var user_id = _user.getUserId();
            try
            {
                await _context.beginTransactionAsync();
                await process(user_id, request.IdTag, request.NomImage, request.descriptionImage, request.Uri);
                result = await _context.SaveChangesAsync(cancellationToken);
                await _context.commitTransactionAsync();

            }
            catch (Exception ex)
            {
                await _context.rollbackTransactionAsync();
                return ManageResult.result(result, "Create_Image", null);

            }
            finally
            {
                await _context.Dispose();
            }

            return ManageResult.result(result, "Create_Image", null);
        }

        private async Task process(string user_id, int idTag, string NomImage, string descriptionImage, string Uri)
        {
            await creationImage(user_id, idTag, NomImage,descriptionImage, Uri); ;
            await changeTagCount(idTag);
        }

        private async Task creationImage(string user_id,int idTag,string NomImage, string descriptionImage, string Uri)
        {
            var entity = new Image()
            {
                NomImage = NomImage,
                descriptionImage = descriptionImage,
                PathImage = "path not available",//CreateImageFile.PathImage(request.NomImage, CreateImageFile.GetFormat(format)),
                IdTag = idTag,
                Uri = Uri,
                DateCreation = DateTime.Now,
                user_id = user_id
            };
            await _context.Images.AddAsync(entity);
        }

        private async Task changeTagCount(int idTag)
        {
            var tag = await _context.Tag
                .FindAsync(idTag);
            tag.numberRef++;
            _context.Tag.Update(tag);
        }

    }

}
