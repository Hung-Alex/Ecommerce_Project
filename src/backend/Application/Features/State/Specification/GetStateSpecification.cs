using Application.DTOs.Filters.State;
using Application.Utils;
using Domain.Entities;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.State.Specification
{
    public class GetStateSpecification : BaseSpecification<Status>
    {
        private readonly StateFilter _filter;
        public GetStateSpecification(StateFilter filter)
        {
            _filter = filter;
            Handler();
        }
        public override Expression<Func<Status, bool>> Criteria =>
            s => (string.IsNullOrEmpty(_filter.Type) || s.Type == _filter.Type) &&
                 (string.IsNullOrEmpty(_filter.Code) || s.Code == _filter.Code);

        protected override void Handler()
        {
            ApplyPaging(_filter.PageSize, _filter.PageNumber);
            if (PredicatedProperty.IsExitedProperty<Status>(_filter.SortColoumn))
            {
                var property = PredicatedProperty.BuildProperty<Status>(_filter.SortColoumn);
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
