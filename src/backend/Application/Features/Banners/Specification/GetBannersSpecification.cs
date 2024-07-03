using Domain.Specifications;
using System.Linq.Expressions;
using Application.Utils;
using Domain.Entities.Banners;
using Application.DTOs.Filters.Banner;

namespace Application.Features.Banners.Specification
{
    public class GetBannersSpecification : BaseSpecification<Banner>
    {
        private readonly BannerFilter _filter;
        public GetBannersSpecification(BannerFilter filter)
        {
            _filter = filter;
            Handler();
        }
        public override Expression<Func<Banner, bool>> Criteria
            => p
            => (string.IsNullOrEmpty(_filter.Title) || p.Title.Contains(_filter.Title));

        protected override void Handler()
        {
            ApplyPaging(_filter.PageSize, _filter.PageNumber);
            if (PredicatedProperty.IsExitedProperty<Banner>(_filter.SortColoumn))
            {
                var property = PredicatedProperty.BuildProperty<Banner>(_filter.SortColoumn);
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
