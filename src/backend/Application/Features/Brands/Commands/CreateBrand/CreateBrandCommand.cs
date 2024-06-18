using Domain.Behavior;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public record CreateBrandCommand(string Name, string Description, string UrlSlug, IFormFile FormFile) : IRequest, IValidatableRequest;
}
