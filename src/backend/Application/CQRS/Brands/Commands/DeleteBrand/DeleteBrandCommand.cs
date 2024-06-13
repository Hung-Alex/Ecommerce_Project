using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Brands.Commands.DeleteBrand
{
    public record DeleteBrandCommand(Guid Id) : IRequest;
}
