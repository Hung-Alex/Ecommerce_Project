using Domain.Entities.Products;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Products.Specification
{
    public sealed class GetProductWithVariantsSpecification : BaseSpecification<Product>
    {
        private readonly Guid _id;
        public GetProductWithVariantsSpecification(Guid productId)
        {
            _id = productId;
            AddInclude(p => p.ProductSkus);
        }
        public override Expression<Func<Product, bool>> Criteria => p => p.Id == _id;
    }
}
