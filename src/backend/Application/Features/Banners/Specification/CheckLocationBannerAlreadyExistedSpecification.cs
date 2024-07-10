using Domain.Entities.Banners;
using Domain.Specifications;
using System.Linq.Expressions;
using static Domain.Enums.BannerEnum;

namespace Application.Features.Banners.Specification
{
    public class CheckLocationBannerAlreadyExistedSpecification : BaseSpecification<Banner>
    {
        private readonly LocationBanner? _locationBanner;
        private readonly Guid? _id;
        public CheckLocationBannerAlreadyExistedSpecification(LocationBanner? locationBanner, Guid? id)
        {
            _id = id;
            _locationBanner = locationBanner;
        }
        public override Expression<Func<Banner, bool>> Criteria => b => b.Location == _locationBanner && b.Id != _id;
    }
}
