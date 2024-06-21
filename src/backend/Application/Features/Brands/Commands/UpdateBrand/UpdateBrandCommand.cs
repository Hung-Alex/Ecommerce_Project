using Application.DTOs.Responses.Brand;
using Domain.Behavior;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Brands.Commands.UpdateBrand
{
    public record UpdateBrandCommand(Guid Id,string Name, string Description, string UrlSlug, IFormFile? Image) : IRequest<BrandDTOs>,IValidatableRequest;
}
