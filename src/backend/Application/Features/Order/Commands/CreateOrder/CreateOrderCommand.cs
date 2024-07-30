using Domain.Enums;
using Domain.Shared;
using MediatR;

namespace Application.Features.Order.Commands.CreateOrder
{
    public record CreateOrderCommand : IRequest<Result<bool>>
    {
        public string Name { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }
        public string Address { get; init; }
        public string? Note { get; init; }
        public PaymentMethod PaymentMethod { get; init; }
    }
}
