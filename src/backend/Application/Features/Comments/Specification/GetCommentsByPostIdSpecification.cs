using Application.DTOs.Filters.Comments;
using Application.Utils;
using Domain.Entities.Brands;
using Domain.Entities.Comments;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Comments.Specification
{
    internal class GetCommentsByPostIdSpecification : BaseSpecification<Comment>
    {
        private readonly CommentFilter _filter;
        public GetCommentsByPostIdSpecification(CommentFilter commentFilter)
        {
            _filter = commentFilter;
            Handler();
        }
        public override Expression<Func<Comment, bool>> Criteria => r => r.PostId == _filter.ProductId;
        protected override void Handler()
        {
            AddInclude(x => x.CreatedByUser);

            ApplyPaging(_filter.PageSize, _filter.PageNumber);
            if (PredicatedProperty.IsExitedProperty<Comment>(_filter.SortColoumn))
            {
                var property = PredicatedProperty.BuildProperty<Comment>(_filter.SortColoumn);
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
