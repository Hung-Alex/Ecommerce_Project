using Domain.Entities.Products;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Products.Specification
{
    public class GetProductByIdSepecification : BaseSpecification<Product>
    {
        private readonly Guid _id;
        public GetProductByIdSepecification(Guid id)
        {
            _id = id;
        }
        public override Expression<Func<Product, bool>> Criteria => x => x.Id == _id;
    }
}
