﻿using Application.Common.Interface;
using Application.Features.State.Specification;
using Domain.Constants;
using Domain.Entities;
using Domain.Entities.Orders;
using Domain.Shared;
using MediatR;

namespace Application.Features.Orders.Commands.CancelOrder
{
    public sealed class CancelOrderCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CancelOrderCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var repoOrder = unitOfWork.GetRepository<Order>();
            var repoStatus = unitOfWork.GetRepository<Status>();
            var cancelStatus = await repoStatus.FindOneAsync(new GetStateByTypeAndCodeSpecification(StateConstants.OrderType, StateConstants.OrderState.Cancelled));
            var order = await repoOrder.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.OrderId));
            }
            order.StatusId = cancelStatus.Id;
            order.CancelReason = request.CancelReason;
            repoOrder.Update(order);
            await unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
