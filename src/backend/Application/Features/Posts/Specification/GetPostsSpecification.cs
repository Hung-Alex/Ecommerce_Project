using Domain.Specifications;
using System.Linq.Expressions;
using Application.Utils;
using Domain.Entities.Posts;
using Application.DTOs.Filters.Posts;

namespace Application.Features.Posts.Specification
{
    public class GetPostsSpecification : BaseSpecification<Post>
    {
        private readonly PostFilter _filter;
        public GetPostsSpecification(PostFilter filter)
        {
            _filter = filter;
            Handler();
        }
        public override Expression<Func<Post, bool>> Criteria
            => p
            => (string.IsNullOrEmpty(_filter.Title) || p.Title.Contains(_filter.Title));

        protected override void Handler()
        {
            ApplyPaging(_filter.PageSize, _filter.PageNumber);
            if (PredicatedProperty.IsExitedProperty<Post>(_filter.SortColoumn))
            {
                var property = PredicatedProperty.BuildProperty<Post>(_filter.SortColoumn);
                switch (_filter.SortBy)
                {
                    case "ASC":
                        ApplyOrderBy(property);
                        break;
                    case "DESC":
                        ApplyOrderByDescending(property);
                        break;
                    default:
                        ApplyOrderBy(b => b.Id);
                        break;
                }
            }
            else
            {
                ApplyOrderBy(b => b.Id);
            }
            base.Handler();
        }
    }
}
