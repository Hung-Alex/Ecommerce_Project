using Application.DTOs.Responses.Category;
using MediatR;

namespace Application.Features.Category.Queries.GetById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDTO>;
}
