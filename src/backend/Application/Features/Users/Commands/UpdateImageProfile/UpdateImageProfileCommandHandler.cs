using Application.Common.Exceptions;
using Application.Common.Interface;
using Application.DTOs.Internal;
using Domain.Constants;
using Domain.Entities.Users;
using Domain.Shared;
using MediatR;

namespace Application.Features.Users.Commands.UpdateImageProfile
{
    public sealed class UpdateImageProfileCommandHandler(IUnitOfWork unitOfWork, IMedia media) : IRequestHandler<UpdateImageProfileCommand, Result<bool>>
    {

        public async Task<Result<bool>> Handle(UpdateImageProfileCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<User>();
            var user = await repo.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.UserError.UserNotFoundWithID(request.UserId));
            }
            Result<ImageUpload> uploadResult = await media.UploadLoadImageAsync(request.Image, UploadFolderConstants.FolderImage);
            if (uploadResult.IsSuccess is false)
            {
                throw new UploadImageException(uploadResult.Errors.Select(x => x.Description).ToList());
            }
            user.AvatarImage = uploadResult.Data.PublicId;
            await unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
