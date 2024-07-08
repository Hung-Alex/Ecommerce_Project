using Domain.Entities.Products;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Products.Specification
{
    public class GetProductDetailSpecification : BaseSpecification<Product>
    {
        private readonly Guid _id;
        public GetProductDetailSpecification(Guid id)
        {
            _id = id;
            Handler();
        }
        protected override void Handler()
        {
            AddInclude(x => x.ProductSkus);
            AddIncludeString("Images.Image");
            AddIncludeString("ProductSubCategories.Category");
            base.Handler();
        }
        public override Expression<Func<Product, bool>> Criteria => p => p.Id == _id;
    }
}
