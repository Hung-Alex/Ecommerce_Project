using Domain.Entities.Products;
using Domain.Specifications;
using System.Linq.Expressions;


namespace Application.Features.Products.Specification
{
    public class UrlSlugIsExistedSpecification : BaseSpecification<Product>
    {
        private readonly string _slug;
        private readonly Guid _id;
        public UrlSlugIsExistedSpecification(Guid id, string urlslug)
        {
            _id = id;
            _slug = urlslug;
        }
        public override Expression<Func<Product, bool>> Criteria => p => p.Id != _id && p.UrlSlug == _slug;
    }
}
