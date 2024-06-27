using Domain.Entities.Banner;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Banners.Specification
{
    public class GetBannerByIdSepecification : BaseSpecification<Banner>
    {
        private readonly Guid _id;
        public GetBannerByIdSepecification(Guid id)
        {
            _id = id;
        }
        public override Expression<Func<Banner, bool>> Criteria => x => x.Id == _id;
    }
}
