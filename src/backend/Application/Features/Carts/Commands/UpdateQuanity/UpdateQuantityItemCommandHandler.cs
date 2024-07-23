using Application.Common.Interface;
using Application.Features.Carts.Specification;
using Domain.Constants;
using Domain.Entities.Carts;
using Domain.Entities.Products;
using Domain.Shared;
using MediatR;

namespace Application.Features.Carts.Commands.UpdateQuanity
{
    public sealed class UpdateQuantityItemCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateQuantityItemCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateQuantityItemCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Cart>();
            var cart = await repo.FindOneAsync(new GetCartByUserIdSpecification(request.UserId));
            if (cart is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.CartError.CartNotFound);
            }
            var repoProduct = unitOfWork.GetRepository<Product>();
            cart.UpdateQuantity(request.CartItemId, request.Quantity);
            await unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
