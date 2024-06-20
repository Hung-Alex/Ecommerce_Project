using Domain.Entities.Category;
using Domain.Specifications;
using System.Linq.Expressions;


namespace Application.Features.Category.Specification
{
    public class UrlSlugIsExistedSpecification : BaseSpecification<Categories>
    {
        private readonly string _slug;
        private readonly Guid _id;
        public UrlSlugIsExistedSpecification(Guid id, string urlslug)
        {
            _id = id;
            _slug = urlslug;
        }
        public override Expression<Func<Categories, bool>> Criteria => p => p.Id != _id && p.UrlSlug == _slug;
    }
}
