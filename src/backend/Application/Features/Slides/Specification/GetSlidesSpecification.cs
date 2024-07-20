using Domain.Specifications;
using System.Linq.Expressions;
using Application.Utils;
using Application.DTOs.Filters.Slides;
using Domain.Entities.Slides;


namespace Application.Features.Slides.Specification
{
    public class GetSlidesSpecification : BaseSpecification<Slide>
    {
        private readonly SlideFilter _filter;
        public GetSlidesSpecification(SlideFilter filter)
        {
            _filter = filter;
            Handler();
        }
        public override Expression<Func<Slide, bool>> Criteria
            => p
            => (string.IsNullOrEmpty(_filter.Title) || p.Title.Contains(_filter.Title));

        protected override void Handler()
        {
            ApplyPaging(_filter.PageSize, _filter.PageNumber);
            if (PredicatedProperty.IsExitedProperty<Slide>(_filter.SortColoumn))
            {
                var property = PredicatedProperty.BuildProperty<Slide>(_filter.SortColoumn);
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
