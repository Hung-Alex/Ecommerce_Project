using Domain.Entities.Tags;
using Domain.Specifications;
using System.Linq.Expressions;


namespace Application.Features.Tags.Specification
{
    public class UrlSlugIsExistedSpecification : BaseSpecification<Tag>
    {
        private readonly string _slug;
        private readonly Guid _id;
        public UrlSlugIsExistedSpecification(Guid id, string urlslug)
        {
            _id = id;
            _slug = urlslug;
        }
        public override Expression<Func<Tag, bool>> Criteria => p => p.Id != _id && p.UrlSlug == _slug;
    }
}
