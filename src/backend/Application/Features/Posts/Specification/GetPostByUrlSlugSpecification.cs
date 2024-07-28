using Domain.Entities.Posts;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Posts.Specification
{
    public class GetPostByUrlSlugSpecification : BaseSpecification<Post>
    {
        private readonly string _urlSlug;
        public GetPostByUrlSlugSpecification(string urlSlug)
        {
            _urlSlug = urlSlug;
            AddInclude(x => x.CreatedByUser);
        }
        public override Expression<Func<Post, bool>> Criteria => p => p.UrlSlug == _urlSlug;
    }
}
