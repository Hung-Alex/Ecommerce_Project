using Domain.Specifications;
using System.Linq.Expressions;
using Application.Utils;
using Domain.Entities.Category;
using Application.DTOs.Filters.Categories;

namespace Application.Features.Category.Specification
{
    public class GetCategoriesSpecification : BaseSpecification<Categories>
    {
        private readonly CategoryFilter _filter;
        public GetCategoriesSpecification(CategoryFilter filter)
        {
            _filter = filter;
            Handler();
        }
        public override Expression<Func<Categories, bool>> Criteria
            => p
            => (string.IsNullOrEmpty(_filter.Name) || p.Name.Contains(_filter.Name)) && p.ParrentId == null;


        protected override void Handler()
        {
            var mode = _filter.Mode;
            string include = "SubCategories";
            while (mode > 1)
            {
                include = include + ".SubCategories";
                --mode;
            }
            if (mode > 0)
            {
                AddIncludeString(include);

            }
            ApplyPaging(_filter.PageSize, _filter.PageNumber);
            if (PredicatedProperty.IsExitedProperty<Categories>(_filter.SortColoumn))
            {
                var property = PredicatedProperty.BuildProperty<Categories>(_filter.SortColoumn);
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
