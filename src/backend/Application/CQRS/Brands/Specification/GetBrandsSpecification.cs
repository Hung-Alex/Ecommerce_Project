using Domain.Entities.Brands;
using Domain.Specifications;
using System.Linq.Expressions;
using Application.Utils;
using Application.DTOs.Filters.Brand;

namespace Application.CQRS.Brands.Specification
{
    public class GetBrandsSpecification : BaseSpecification<Brand>
    {
        private readonly BrandFilter _filter;
        public GetBrandsSpecification(BrandFilter filter)
        {
            _filter = filter;
            Handler();
        }
        public override Expression<Func<Brand, bool>> Criteria
            => p
            => (string.IsNullOrEmpty(_filter.Name) || p.Name.Contains(_filter.Name));

        protected override void Handler()
        {
            ApplyPaging(_filter.PageSize, _filter.PageNumber);
            if (PredicatedProperty.IsExitedProperty<Brand>(_filter.SortColoumn))
            {
                var property = PredicatedProperty.BuildProperty<Brand>(_filter.SortColoumn);
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
