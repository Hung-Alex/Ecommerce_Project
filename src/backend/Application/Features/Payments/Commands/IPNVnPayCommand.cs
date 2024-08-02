using Domain.Shared;
using MediatR;
using System.Transactions;

namespace Application.Features.Payments.Commands
{
    public class IPNVnPayCommand : IRequest<Result<bool>>
    {
        public Guid OrderId { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string TransactionId { get; set; }
    }
}
