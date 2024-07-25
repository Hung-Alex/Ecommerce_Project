using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Banners;
using Domain.Entities.Category;
using Domain.Shared;
using MediatR;

namespace Application.Features.Category.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMedia _media;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IMedia media)
        {
            _unitOfWork = unitOfWork;
            _media = media;
        }
        public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var repoCategory = _unitOfWork.GetRepository<Categories>();
            var category = await repoCategory.GetByIdAsync(request.Id);
            if (category == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            }
            var DeleteImageResult = await _media.DeleteImageAsync(category.PublicIdImage);
            if (DeleteImageResult.IsSuccess is false)
            {
                return Result<bool>.ResultFailures(DeleteImageResult.Errors);
            }
            repoCategory.Delete(category);
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
