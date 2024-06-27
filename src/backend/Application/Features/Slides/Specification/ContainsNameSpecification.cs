using Domain.Entities.Slides;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Slides.Specification
{
    public class ContainsNameSpecification : BaseSpecification<Slide>
    {
        private readonly string _title;
        public ContainsNameSpecification(string title)
        {
            _title = title;
        }
        public override Expression<Func<Slide, bool>> Criteria => b => b.Title.Contains(_title);
    }
}
