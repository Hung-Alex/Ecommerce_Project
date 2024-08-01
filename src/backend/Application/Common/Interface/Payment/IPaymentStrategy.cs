using Application.DTOs.Responses.Payments;
using Domain.Entities.Orders;
using Domain.Enums;
using Domain.Shared;

namespace Application.Common.Interface.Payment
{
    public interface IPaymentStrategy
    {
        Task<Result<PaymentsResultDTO>> CreatePaymentUrl(Order order, CancellationToken cancellationToken);
    }
}
