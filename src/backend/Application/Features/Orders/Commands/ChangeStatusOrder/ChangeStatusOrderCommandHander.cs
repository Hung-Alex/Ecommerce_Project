using Application.Common.Exceptions;
using Application.Common.Interface;
using Domain.Constants;
using Domain.Entities;
using Domain.Entities.Orders;
using Domain.Shared;
using MediatR;

namespace Application.Features.Orders.Commands.ChangeStatusOrder
{
    public sealed class ChangeStatusOrderCommandhander(IUnitOfWork unitOfWork) : IRequestHandler<ChangeStatusOrderCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(ChangeStatusOrderCommand request, CancellationToken cancellationToken)
        {
            var repoOrder = unitOfWork.GetRepository<Order>();
            var repoStatus = unitOfWork.GetRepository<Status>();

            var status = await repoStatus.GetByIdAsync(request.StatusId);
            if (status is null || !(status.Type == StateConstants.OrderType))
            {
                throw new ValidationException("Invalid Status");
            }
            var order = await repoOrder.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.OrderId));
            }
            order.StatusId = status.Id;
            await unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
