using Domain.Entities.Slides;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Slides.Specification
{
    public class GetSlideByIdSepecification : BaseSpecification<Slide>
    {
        private readonly Guid _id;
        public GetSlideByIdSepecification(Guid id)
        {
            _id = id;
            AddInclude(x => x.SlidesImages);
        }
        public override Expression<Func<Slide, bool>> Criteria => x => x.Id == _id;
    }
}
