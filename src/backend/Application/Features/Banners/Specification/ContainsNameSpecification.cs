using Domain.Entities.Banner;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Banners.Specification
{
    public class ContainsNameSpecification : BaseSpecification<Banner>
    {
        private readonly string _title;
        public ContainsNameSpecification(string title)
        {
            _title = title;
        }
        public override Expression<Func<Banner, bool>> Criteria => b => b.Title.Contains(_title);
    }
}
