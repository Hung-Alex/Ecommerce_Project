using Domain.Specifications;
using System.Linq.Expressions;
using Application.Utils;
using Domain.Entities.Tags;
using Application.DTOs.Filters.Tags;

namespace Application.Features.Tags.Specification
{
    public class GetTagsSpecification : BaseSpecification<Tag>
    {
        private readonly TagFilter _filter;
        public GetTagsSpecification(TagFilter filter)
        {
            _filter = filter;
            Handler();
        }
        public override Expression<Func<Tag, bool>> Criteria
            => p
            => (string.IsNullOrEmpty(_filter.Name) || p.Name.Contains(_filter.Name));

        protected override void Handler()
        {
            ApplyPaging(_filter.PageSize, _filter.PageNumber);
            if (PredicatedProperty.IsExitedProperty<Tag>(_filter.SortColoumn))
            {
                var property = PredicatedProperty.BuildProperty<Tag>(_filter.SortColoumn);
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
