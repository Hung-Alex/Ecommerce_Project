using Domain.Entities.WishLists;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.WishsList.Specification
{
    public class GetListFavoriteProductByUserIdSepecification : BaseSpecification<WishList>
    {
        private readonly Guid _userId;
        public GetListFavoriteProductByUserIdSepecification(Guid userId)
        {
            _userId = userId;
        }
        protected override void Handler()
        {
            AddInclude(x => x.Product);
            base.Handler();
        }
        public override Expression<Func<WishList, bool>> Criteria => x => x.UserId == _userId;
    }
}
