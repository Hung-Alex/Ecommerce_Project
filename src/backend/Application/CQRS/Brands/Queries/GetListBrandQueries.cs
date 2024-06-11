using Domain.Entities.Brands;
using Domain.Interface;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Brands.Queries
{
    public record GetListBrandQueries(IPagingParams PagingParams, string Name) : IRequest<Result<IEnumerable<Brand>>>;

}
