using Application.DTOs.Filters.Search;
using Application.Utils;
using Domain.Entities.Products;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Search.Specification
{
    public class FilterProductSpecification : BaseSpecification<Product>
    {
        private readonly SearchFilter _filter;
        public FilterProductSpecification(SearchFilter searchFilter)
        {
            _filter = searchFilter;
            Handler();
        }
        public override Expression<Func<Product, bool>> Criteria
             => p
            => (string.IsNullOrEmpty(_filter.UrlSlugCategory) || p.Category.UrlSlug == _filter.UrlSlugCategory)
             && (string.IsNullOrEmpty(_filter.UrlSlugBrand) || p.Brand.UrlSlug == _filter.UrlSlugBrand)
             && (string.IsNullOrEmpty(_filter.ProductName) || p.Name.Contains(_filter.ProductName));
        protected override void Handler()
        {
            AddInclude(x => x.Images);
            AddInclude(x => x.Category);
            AddInclude(x => x.Brand);
            AddInclude(x => x.Rattings);
            ApplyPaging(_filter.PageSize, _filter.PageNumber);
            if (PredicatedProperty.IsExitedProperty<Product>(_filter.SortColoumn))
            {
                var property = PredicatedProperty.BuildProperty<Product>(_filter.SortColoumn);
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
