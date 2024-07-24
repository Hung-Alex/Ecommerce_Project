using Application.DTOs.Responses.Product.Shared.Variants;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.AddProductVariant
{
    public record AddProductVariantCommand(Guid ProductId,string Name,string Description):IRequest<Result<bool>>;
}
