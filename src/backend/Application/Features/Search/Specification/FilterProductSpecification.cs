

using Domain.Entities.Products;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Search.Specification
{
    public class FilterProductSpecification : BaseSpecification<Product>
    {
        public override Expression<Func<Product, bool>> Criteria => throw new NotImplementedException();
    }
}
