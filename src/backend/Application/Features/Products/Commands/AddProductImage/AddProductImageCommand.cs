using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Products.Commands.AddProductImage
{
    public record AddProductImageCommand(Guid ProductId, IFormFile File) : IRequest<Result<bool>>;
}
