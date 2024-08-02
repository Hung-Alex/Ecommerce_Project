using Application.DTOs.Responses.Payments;
using Domain.Entities.Orders;
using Domain.Entities.Payments;
using Domain.Shared;

namespace Application.Common.Interface.Payments
{
    public interface IVnPayService
    {
        Task<Result<PaymentsResultDTO>> CreatePaymentUrl(Order order,Payment payment,decimal amount, CancellationToken cancellationToken);
    }
}
