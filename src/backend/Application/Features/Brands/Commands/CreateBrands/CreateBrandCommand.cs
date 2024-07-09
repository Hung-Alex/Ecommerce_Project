using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Brands.Commands.CreateBrands
{
    public record CreateBrandCommand(string Name, string Description, string UrlSlug, IFormFile FormFile) : IRequest<Result<bool>>, IValidatableRequest;
}
