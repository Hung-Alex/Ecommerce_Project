using Application.Common.Interface;
using Application.Features.Carts.Specification;
using Domain.Entities.Carts;
using Domain.Shared;
using MediatR;

namespace Application.Features.Carts.Commands.AddItem
{
    public sealed class AddItemCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddItemCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<Cart>();
            var cart = await repo.FindOneAsync(new GetCartByUserIdSpecification(request.UserId));
            var item = cart.CreateCartItem(request.ProductId, request.ProductSkusId, request.Quantity);
            cart.AddItems(item);
            await unitOfWork.Commit();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
