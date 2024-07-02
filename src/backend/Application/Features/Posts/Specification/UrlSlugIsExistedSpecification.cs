using Domain.Entities.Posts;
using Domain.Specifications;
using System.Linq.Expressions;


namespace Application.Features.Posts.Specification
{
    public class UrlSlugIsExistedSpecification : BaseSpecification<Post>
    {
        private readonly string _slug;
        private readonly Guid _id;
        public UrlSlugIsExistedSpecification(Guid id, string urlslug)
        {
            _id = id;
            _slug = urlslug;
        }
        public override Expression<Func<Post, bool>> Criteria => p => p.Id != _id && p.UrlSlug == _slug;
    }
}
