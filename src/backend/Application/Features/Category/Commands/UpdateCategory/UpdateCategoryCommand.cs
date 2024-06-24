using Application.DTOs.Responses.Category;
using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Category.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id, string Name, string Description, string UrlSlug, IFormFile? Image) : IRequest<Result<CategoryDTO>>, IValidatableRequest;
}
