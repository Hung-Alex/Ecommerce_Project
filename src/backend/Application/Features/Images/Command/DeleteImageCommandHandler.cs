using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.Features.Images.Command
{
    public sealed class DeleteImageCommandHandler(IUnitOfWork unitOfWork, IMedia media) : IRequestHandler<DeleteImageCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Image>();
            var image = await repo.GetByIdAsync(request.id);
            if (image == null) { return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.id)); }
            var isDelete = await media.DeleteImageAsync(image.PublicId);
            if (isDelete.IsSuccess is false)
            {
                return Result<bool>.ResultFailures(isDelete.Errors);
            }
            repo.Delete(image);
            await unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
