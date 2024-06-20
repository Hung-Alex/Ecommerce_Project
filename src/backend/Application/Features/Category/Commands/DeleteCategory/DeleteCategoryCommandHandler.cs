using Application.Common.Exceptions;
using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities.Category;
using MediatR;

namespace Application.Features.Category.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var repoCategory = _unitOfWork.GetRepository<Categories>();
            var category = await repoCategory.GetByIdAsync(request.Id);
            if (category == null)
            {
                throw new NotFoundException($"{ErrorConstants.NotFound}{request.Id}");
            }
            repoCategory.Delete(category);
            await _unitOfWork.Commit();
        }
    }
}
