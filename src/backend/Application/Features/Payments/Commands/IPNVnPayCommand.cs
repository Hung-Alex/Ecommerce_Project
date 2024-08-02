using Domain.Shared;
using MediatR;

namespace Application.Features.Payments.Commands
{
    public record IPNVnPayCommand : IRequest<Result<bool>>
    {
        public Guid OrderId { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string TransactionId { get; set; }
    }
}
