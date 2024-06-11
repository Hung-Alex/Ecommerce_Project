using Domain.Behavior;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Brand.Commands.CreateBrand
{
    public record CreateBrandCommand(string Name, string Description, string UrlSlug, IFormFile FormFile) : IRequest, IValidatableRequest;

}
