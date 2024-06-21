using Application.DTOs.Responses.Product;
using Domain.Behavior;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, string Description, string UrlSlug, IFormFile? Image) : IRequest<ProductDTO>,IValidatableRequest;
}
