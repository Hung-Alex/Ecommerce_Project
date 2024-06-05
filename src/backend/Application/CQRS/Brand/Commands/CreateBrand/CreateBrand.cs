using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Brand.Commands.CreateBrand
{
    public record CreateBrandCommand:IRequest
    {
    }
}
