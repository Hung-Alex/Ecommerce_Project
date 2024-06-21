using Domain.Behavior;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.Commands.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, string UrlSlug, IFormFile FormFile) : IRequest, IValidatableRequest;
}
