﻿using Domain.Behavior;
using Domain.Shared;
using MediatR;

namespace Application.Features.Carts.Commands.AddItem
{
    public record AddItemCommand(Guid UserId, Guid ProductId, int Quantity) : IRequest<Result<bool>>,IValidatableRequest;
}
