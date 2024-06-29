using Domain.Specifications;
using System.Linq.Expressions;
using Application.Utils;
using Application.DTOs.Filters.Categories;
using Domain.Entities.Images;

namespace Application.Features.Images.Specification
{
    public class GetImagesSpecification : BaseSpecification<Image>
    {
        private readonly CategoryFilter _filter;
        public GetImagesSpecification(CategoryFilter filter)
        {
            _filter = filter;
            Handler();
        }
        public override Expression<Func<Image, bool>> Criteria => null;

        protected override void Handler()
        {
            ApplyPaging(_filter.PageSize, _filter.PageNumber);
            if (PredicatedProperty.IsExitedProperty<Image>(_filter.SortColoumn))
            {
                var property = PredicatedProperty.BuildProperty<Image>(_filter.SortColoumn);
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
