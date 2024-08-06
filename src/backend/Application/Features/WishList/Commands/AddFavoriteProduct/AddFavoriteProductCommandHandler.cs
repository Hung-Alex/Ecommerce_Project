using Application.Common.Interface;
using Application.Features.WishLists.Specification;
using Domain.Constants;
using Domain.Entities.Products;
using Domain.Entities.WishLists;
using Domain.Shared;
using FluentValidation;
using MediatR;


namespace Application.Features.WishsList.Commands.CreateFavoriteProduct
{
    public sealed class AddFavoriteProductCommandHandler : IRequestHandler<AddFavoriteProductCommand, Result<bool>>
    {
        internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommandValidator>
        {

        }
        private readonly IUnitOfWork _unitOfWork;

        public AddFavoriteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(AddFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            var repoWishList = _unitOfWork.GetRepository<WishList>();
            var IsExistedInWishList = await repoWishList.FindOneAsync(new ProductIsExistedInWishListSpecification(request.ProductId, request.UserId));
            if (IsExistedInWishList is not null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.WishListError.WishListProductIsExistedInWishListWithId(request.ProductId));
            }
            var repo = _unitOfWork.GetRepository<Product>();
            var hasProduct = await repo.GetByIdAsync(request.ProductId);
            if (hasProduct is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.ProductId));
            }
            repoWishList.Add(new WishList() { ProductId = request.ProductId, UserId = request.UserId });
            await _unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
