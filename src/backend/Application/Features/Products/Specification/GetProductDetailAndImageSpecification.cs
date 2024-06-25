using Domain.Entities.Products;
using Domain.Specifications;
using System.Linq.Expressions;


namespace Application.Features.Products.Specification
{
    public class GetProductDetailAndImageSpecification : BaseSpecification<Product>
    {
        private readonly Guid _id;
        public GetProductDetailAndImageSpecification(Guid id)
        {
            _id = id;
            AddIncludeString("Images.Image");
        }
        public override Expression<Func<Product, bool>> Criteria => p => p.Id == _id;
    }
}
