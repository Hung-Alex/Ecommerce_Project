using Domain.Entities.Slides;
using Domain.Specifications;
using System.Linq.Expressions;


namespace Application.Features.Slides.Specification
{
    public class GetSlideDetailAndImageSpecification : BaseSpecification<Slide>
    {
        private readonly Guid _id;
        public GetSlideDetailAndImageSpecification(Guid id)
        {
            _id = id;
        }
        public override Expression<Func<Slide, bool>> Criteria => p => p.Id == _id;
    }
}
