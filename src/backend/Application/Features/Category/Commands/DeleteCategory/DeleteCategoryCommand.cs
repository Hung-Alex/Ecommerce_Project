using Domain.Shared;
using MediatR;

namespace Application.Features.Category.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<Result<bool>>;
}
