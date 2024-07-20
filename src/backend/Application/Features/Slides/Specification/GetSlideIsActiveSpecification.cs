using Domain.Entities.Slides;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Slides.Specification
{
    public class GetSlideIsActiveSpecification : BaseSpecification<Slide>
    {
        public GetSlideIsActiveSpecification()
        {
        }
        public override Expression<Func<Slide, bool>> Criteria => s => s.IsActive == true;
    }
}
