using Domain.Enums;
using Domain.Shared;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(string Name, string Email, string Phone, string Address, string? Note, PaymentMethod PaymentMethod) : IRequest<Result<bool>>
    {
        public Guid UserId { get; init; }

    }
}
