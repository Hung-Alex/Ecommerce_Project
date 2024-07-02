using Domain.Entities.WishLists;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.WishLists.Specification
{
    public class ProductIsExistedInWishListSpecification : BaseSpecification<WishList>
    {
        private readonly Guid _productId;
        private readonly Guid _userId;
        public ProductIsExistedInWishListSpecification(Guid productId, Guid userId)
        {
            _productId = productId;
            _userId = userId;
        }
        public override Expression<Func<WishList, bool>> Criteria => w => w.UserId == _userId && w.ProductId == _productId;
    }
}
