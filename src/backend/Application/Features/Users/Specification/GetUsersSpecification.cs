using Application.DTOs.Filters.Users;
using Application.Utils;
using Domain.Entities.Users;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Users.Specification
{
    public class GetUsersSpecification : BaseSpecification<User>
    {
        private readonly UserFilter _filter;
        public GetUsersSpecification(UserFilter filter)
        {
            _filter = filter;
            Handler();
        }
        public override Expression<Func<User, bool>> Criteria => u => (string.IsNullOrEmpty(_filter.Name) || u.FirstName.Contains(_filter.Name));
        public void Handler()
        {
            ApplyPaging(_filter.PageSize, _filter.PageNumber);
            if (PredicatedProperty.IsExitedProperty<User>(_filter.SortColoumn))
            {
                var property = PredicatedProperty.BuildProperty<User>(_filter.SortColoumn);
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
