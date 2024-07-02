using Application.DTOs.Filters.Categories;
using Application.DTOs.Responses.Category;
using Domain.Shared;
using MediatR;

namespace Application.Features.Images.Queries.Get
{
    public record GetListImageQuery(CategoryFilter CategoryFilter) : IRequest<Result<IEnumerable<CategoryDTO>>>;
}
