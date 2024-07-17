using Application.DTOs.Responses.Brands;
using Domain.Behavior;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Brands.Commands.UpdateBrand
{
    public record UpdateBrandCommand(Guid Id, string Name, string Description, string UrlSlug, IFormFile? Image) : IRequest<Result<BrandDTO>>, IValidatableRequest;
}
