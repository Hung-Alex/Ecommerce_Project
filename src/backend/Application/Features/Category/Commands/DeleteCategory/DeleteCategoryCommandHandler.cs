using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Category;
using Domain.Shared;
using MediatR;

namespace Application.Features.Category.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var repoCategory = _unitOfWork.GetRepository<Categories>();
            var category = await repoCategory.GetByIdAsync(request.Id);
            if (category == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.Id));
            }
            repoCategory.Delete(category);
            await _unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
