using Application.DTOs.Filters.Posts;
using Application.Utils;
using Domain.Entities.Posts;
using Domain.Specifications;
using System.Linq.Expressions;


namespace Application.Features.Posts.Specification
{
    public class GetPostsPublishedSpecification : BaseSpecification<Post>
    {
        private readonly PostFilter _filter;
        public GetPostsPublishedSpecification(PostFilter filter)
        {
            _filter = filter;
            Handler();
        }
        public override Expression<Func<Post, bool>> Criteria =>
            p
            => (string.IsNullOrEmpty(_filter.Title) || p.Title.Contains(_filter.Title)) && (p.Published == true);
        protected override void Handler()
        {
            AddInclude(x => x.CreatedByUser);
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
