using Application.Common.Interface;
using Application.Features.State.Specification;
using Domain.Constants;
using Domain.Entities;
using Domain.Entities.Orders;
using Domain.Entities.Payments;
using Domain.Shared;
using MediatR;

namespace Application.Features.Payments.Commands
{
    public sealed class IPNVnPayCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<IPNVnPayCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(IPNVnPayCommand request, CancellationToken cancellationToken)
        {
            var repoOrder = unitOfWork.GetRepository<Order>();
            var repoPayment = unitOfWork.GetRepository<Payment>();
            var repoStatus =  unitOfWork.GetRepository<Status>();
            var statusCompleted = await repoStatus.FindOneAsync(new GetStateByTypeAndCodeSpecification(StateConstants.PaymentType, StateConstants.PaymentState.Completed));
            var statusFail = await repoStatus.FindOneAsync(new GetStateByTypeAndCodeSpecification(StateConstants.PaymentType, StateConstants.PaymentState.Failed));        
            var order= await repoOrder.GetByIdAsync(request.OrderId);
            if (order is null)
            {
                return Result<bool>.ResultFailures(ErrorConstants.NotFoundWithId(request.OrderId));
            }
            var payment=await repoPayment.GetByIdAsync(order.PaymentId);
            // check code from vnpay, I just need to check with 00 to succeed, you can check with other code from vnpay for other use cases
            if (request.Code=="00")
            {
                payment.TransactionId = request.TransactionId;
                payment.TransactionDate = DateTimeOffset.Now;
                payment.StatusId = statusCompleted.Id;
                order.StatusId = statusCompleted.Id;
            }
            else
            {
                payment.TransactionId = request.TransactionId;
                payment.TransactionDate = DateTimeOffset.Now;
                payment.StatusId = statusFail.Id;
                order.StatusId = statusFail.Id;// if payment failed, order will be failed too => ^-^ order failed not payment failed <3 , ^-^ ^-^ ,above too
            }
            repoPayment.Update(payment);
            repoOrder.Update(order);
            await unitOfWork.CommitAsync();
            return Result<bool>.ResultSuccess(true);
        }
    }
}
