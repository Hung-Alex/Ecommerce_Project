using Application.Common.Interface;
using Application.Common.Interface.RepositoryExtension;
using Domain.Constants;
using Domain.Entities.Category;
using Domain.Shared;
using MediatR;

namespace Application.Features.Category.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepositoryExtension _categoryRepositoryExtension;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, ICategoryRepositoryExtension categoryRepositoryExtension)
        {
            _unitOfWork = unitOfWork;
            _categoryRepositoryExtension = categoryRepositoryExtension;

        }
        public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var repoCategory = _unitOfWork.GetRepository<Categories>();
            var category = await repoCategory.GetByIdAsync(request.Id);
            if (category == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            }
            await _categoryRepositoryExtension.SoftDeleteCategory(category.Id);
            await _unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
