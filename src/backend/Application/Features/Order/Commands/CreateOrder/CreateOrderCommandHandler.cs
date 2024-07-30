
using Domain.Shared;
using FluentValidation;
using MediatR;

namespace Application.Features.Order.Commands.CreateOrder
{
    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<bool>>
    {
        public class CreateOrderCommandValidator:AbstractValidator<CreateOrderCommand>    
        {
            public CreateOrderCommandValidator() 
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Phone).NotEmpty();
                RuleFor(x => x.Address).NotEmpty();
                RuleFor(x => x.PaymentMethod).IsInEnum();
            }
        }
        public Task<Result<bool>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
