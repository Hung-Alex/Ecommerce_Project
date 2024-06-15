using Application.DTOs.Responses.Brand;
using Domain.Behavior;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.UpdateBrand
{
    public record UpdateBrandCommand(Guid Id,string Name, string Description, string UrlSlug, IFormFile? Image) : IRequest<BrandDTOs>,IValidatableRequest;
}
