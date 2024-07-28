using Domain.Entities.Posts;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Posts.Specification
{
    public class GetPostByIdSepecification : BaseSpecification<Post>
    {
        private readonly Guid _id;
        public GetPostByIdSepecification(Guid id)
        {
            _id = id;
            AddInclude(x => x.CreatedByUser);
        }
        public override Expression<Func<Post, bool>> Criteria => x => x.Id == _id;
    }
}
