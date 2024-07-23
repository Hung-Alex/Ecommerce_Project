using Application.Common.Interface;
using Application.Features.Carts.Specification;
using Domain.Constants;
using Domain.Entities.Carts;
using Domain.Shared;
using MediatR;

namespace Application.Features.Carts.Commands.DeleteItem
{
    public sealed class DeleteItemCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteItemCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Cart>();
            var cart = await repo.FindOneAsync(new GetCartByUserIdSpecification(request.UserId));
            if (cart is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.CartError.CartNotFound);
            }
            var isExited = cart.CartItems.FirstOrDefault(x => x.Id == request.CarItemId);
            if (isExited == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.CarItemId));

            }
            cart.RemoveItem(request.CarItemId);
            await unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
