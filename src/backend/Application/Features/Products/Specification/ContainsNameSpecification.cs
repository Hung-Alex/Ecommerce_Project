using Domain.Entities.Products;
using Domain.Specifications;
using System.Linq.Expressions;

namespace Application.Features.Products.Specification
{
    public class ContainsNameSpecification : BaseSpecification<Product>
    {
        private readonly string _name;
        public ContainsNameSpecification(string name)
        {
            _name = name;
        }
        public override Expression<Func<Product, bool>> Criteria => b => b.Name.Contains(_name);
    }
}
