using Domain.Entities.Products;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Products.Specification
{
    public class GetProductDetailsByUrlSlugSpecification : BaseSpecification<Product>
    {
        private readonly string _slug;
        public GetProductDetailsByUrlSlugSpecification(string slug)
        {
            _slug = slug;
            Handler();
        }
        public override Expression<Func<Product, bool>> Criteria => p => p.UrlSlug == _slug;

        protected override void Handler()
        {
            AddInclude(p => p.Rattings);
            AddInclude(p => p.ProductSkus);
            AddInclude(p => p.Brand);
            AddInclude(p => p.Images);
            AddInclude(p => p.Category);
            base.Handler();
        }
    }
}
