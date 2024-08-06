using Application.Common.Interface;
using Application.Features.WishsList.Specification;
using Domain.Constants;
using Domain.Entities.WishLists;
using Domain.Shared;
using MediatR;

namespace Application.Features.WishsList.Commands.DeleteFavoriteProduct
{
    public sealed class DeleteFavoriteProductCommandHandler : IRequestHandler<DeleteFavoriteProductCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteFavoriteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(DeleteFavoriteProductCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepository<WishList>();
            var favouriteProductsOfUser = await repo.GetAllAsync(new GetListFavoriteProductByUserIdSepecification(request.UserId));
            var hasProductInListWish = favouriteProductsOfUser.Where(x => x.ProductId == request.ProductId).FirstOrDefault();
            if (hasProductInListWish is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.WishListError.ProductDontHaveInWishlistWithId(request.ProductId));
            }
            repo.Delete(hasProductInListWish);
            await _unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
