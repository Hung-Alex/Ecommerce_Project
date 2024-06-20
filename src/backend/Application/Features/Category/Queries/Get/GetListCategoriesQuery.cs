using Application.DTOs.Filters.Categories;
using Application.DTOs.Responses.Category;
using Domain.Shared;
using MediatR;

namespace Application.Features.Category.Queries.Get
{
    public record GetListCategoriesQuery(CategoryFilter CategoryFilter) : IRequest<Result<IEnumerable<CategoryDTO>>>;
}
