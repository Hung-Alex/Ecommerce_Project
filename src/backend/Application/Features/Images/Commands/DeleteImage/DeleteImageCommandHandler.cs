using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Images;
using Domain.Shared;
using MediatR;

namespace Application.Features.Images.Commands.DeleteImage
{
    public sealed class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia _media;
        public DeleteImageCommandHandler(IUnitOfWork unitOfWork, IMedia media)
        {
            _unitOfWork = unitOfWork;
            _media = media; 
        }
        public async Task<Result<bool>> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var repoImage = _unitOfWork.GetRepository<Image>();
            var image = await repoImage.GetByIdAsync(request.Id);
            if (image == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            }
            repoImage.Delete(image);
            await _unitOfWork.Commit();
            await _media.DeleteImageAsync(image.PublicId);
            return Result<bool>.ResultSuccess(true);
        }
    }
}
