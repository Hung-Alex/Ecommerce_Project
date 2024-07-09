using Domain.Shared;
using MediatR;

namespace Application.Features.Brands.Commands.DeleteBrand
{
    public record DeleteBrandCommand(Guid Id) : IRequest<Result<bool>>;
}
