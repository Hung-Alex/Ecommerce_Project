using Domain.Specifications;
using System.Linq.Expressions;
using Application.Utils;
using Domain.Entities.Products;
using Application.DTOs.Filters.Product;


namespace Application.Features.Products.Specification
{
    public class GetProductsSpecification : BaseSpecification<Product>
    {
        private readonly ProductFilter _filter;
        public GetProductsSpecification(ProductFilter filter)
        {
            _filter = filter;
            Handler();
        }
        public override Expression<Func<Product, bool>> Criteria
            => p
            => (string.IsNullOrEmpty(_filter.Name) || p.Name.Contains(_filter.Name));

        protected override void Handler()
        {
            AddInclude(x => x.Images);
            AddInclude(x => x.Brand);
            AddInclude(x => x.Category);
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
            AddInclude(p => p.Images);
            base.Handler();
        }
    }
}
