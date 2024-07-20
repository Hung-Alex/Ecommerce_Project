using Domain.Entities.Rattings;
using Domain.Specifications;
using System.Linq.Expressions;


namespace Application.Features.Rattings.Specification
{
    public class GetRattingProductByIdSpecification : BaseSpecification<Ratting>
    {
        private readonly Guid _productId;
        public GetRattingProductByIdSpecification(Guid productId)
        {
            _productId = productId;
        }
        public override Expression<Func<Ratting, bool>> Criteria => r => r.ProductId == _productId;
    }
}
