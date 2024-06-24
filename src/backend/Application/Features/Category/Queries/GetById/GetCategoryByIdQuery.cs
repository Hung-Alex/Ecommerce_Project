using Application.DTOs.Responses.Category;
using Domain.Shared;
using MediatR;

namespace Application.Features.Category.Queries.GetById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<Result<CategoryDTO>>;
}
