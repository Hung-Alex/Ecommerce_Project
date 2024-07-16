using Domain.Entities.Banners;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Banners.Specification
{
    public class GetBannerIsVisiableSpecification : BaseSpecification<Banner>
    {
        public override Expression<Func<Banner, bool>> Criteria => b => b.IsVisible == true;
    }
}
