using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Posts;
using Domain.Shared;
using MediatR;

namespace Application.Features.Posts.Commands.DeletePost
{
    public sealed class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia _media;
        public DeletePostCommandHandler(IUnitOfWork unitOfWork, IMedia media)
        {
            _unitOfWork = unitOfWork;
            _media = media;
        }
        public async Task<Result<bool>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<Post>();
            var post = await repo.GetByIdAsync(request.Id);
            if (post == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            }
            var DeleteImageResult = await _media.DeleteImageAsync(post.PublicIdImage);
            if (DeleteImageResult.IsSuccess is false)
            {
                return Result<bool>.ResultFailures(DeleteImageResult.Errors);
            }
            repo.Delete(post);
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
