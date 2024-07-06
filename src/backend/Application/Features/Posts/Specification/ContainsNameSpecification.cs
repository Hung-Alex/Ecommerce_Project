using Domain.Entities.Posts;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Posts.Specification
{
    public class ContainsTitleSpecification : BaseSpecification<Post>
    {
        private readonly string _title;
        public ContainsTitleSpecification(string title)
        {
            _title = title;
        }
        public override Expression<Func<Post, bool>> Criteria => b => b.Title.Contains(_title);
    }
}
