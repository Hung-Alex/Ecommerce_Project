using Domain.Shared;
using MediatR;

namespace Application.Features.Category.Commands.AddCategoryForProduct
{
    public record AddCategotyForProductCommand(Guid ParrentCategoryId, Guid? SubCategoryId,Guid ProductId) : IRequest<Result<bool>>;
}
