﻿using Application.DTOs.Responses.Product;
using Domain.Behavior;
using Domain.Shared;
using MediatR;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, string UrlSlug, decimal Price, string UnitPrice, Guid BrandId, int? Discount) : IRequest<Result<ProductDTO>>, IValidatableRequest;
}